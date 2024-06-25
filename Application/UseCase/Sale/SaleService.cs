using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Application.Response;
using Domain.Entities;

namespace Application.UseCase;

public class SaleServices : ISaleServices
{
    private readonly ISaleCommands _command;
    private readonly ISaleQuery _query;
    private readonly IProductServices _productServices;
    private readonly ISaleProductServices _saleProductServices;

    public SaleServices(ISaleCommands command, ISaleQuery query, IProductServices productServices, 
                        ISaleProductServices saleProductServices)
    {
        _command = command;
        _query = query;
        _productServices = productServices;
        _saleProductServices = saleProductServices;
    }
    public async Task<SaleResponse> CreateSale(SaleRequest request)
    {
        Dictionary<ProductResponse, int> productsAndQuantities = await RetrieveProducts(request);
        decimal subTotal = ComputeSubTotal(productsAndQuantities);
        decimal totalDiscount = ComputeTotalDiscount(productsAndQuantities);
        decimal totalPay = ComputeTotal(subTotal, totalDiscount, 1.21m);
        int totalQuantity = 0;
        if(request.TotalPayed == totalPay)
        {
            foreach(KeyValuePair<ProductResponse, int> product in productsAndQuantities)
            {
                totalQuantity += product.Value;
            }
            Sale sale = await InsertSale(totalPay, subTotal, totalDiscount, 1.21m);
            List<SaleProductResponse> saleProductResponses = await InsertSaleProducts(sale, productsAndQuantities);
            return await CreateSaleResponse(sale, totalQuantity, saleProductResponses);
        }
        else
        {
            throw new Conflict("El total es incorrecto");
        }        
    }
    public async Task<Dictionary<ProductResponse, int>> RetrieveProducts(SaleRequest request)
    {
        Dictionary<ProductResponse, int> productsAndQuantities= new Dictionary<ProductResponse, int>();
        try
        {
            foreach(SaleProductRequest saleProduct in request.Products)
            {
                ProductResponse currentProduct = await _productServices.GetProductById(saleProduct.ProductId);
                productsAndQuantities.Add(currentProduct, saleProduct.Quantity);                
            };
            return productsAndQuantities;
        }
        catch (NotFoundException)
        {
            throw new BadRequestException("Producto/s inexistente/s");
        }        
    }
    public decimal ComputeSubTotal(Dictionary<ProductResponse, int> productsAndQuantities)
    {
        decimal total = 0m;
        foreach(KeyValuePair<ProductResponse, int> product in productsAndQuantities)
        {
            total += product.Key.Price * product.Value;
        };
        return total;
    }
    public decimal ComputeTotalDiscount(Dictionary<ProductResponse, int> productsAndQuantities)
    {
        decimal totalDiscount = 0;
        foreach(KeyValuePair<ProductResponse, int> product in productsAndQuantities)
        {
            if(product.Key.Discount > 0)
            {
                decimal percentage = product.Key.Discount / 100m;
                decimal unitDiscount = product.Key.Price * percentage;
                totalDiscount += product.Value*unitDiscount;
            }
        };
        return totalDiscount;
    }
    public decimal ComputeTotal(decimal subTotal, decimal totalDiscount, decimal taxes)
    {
        decimal total = (subTotal - totalDiscount) * taxes;
        return total;
    }
    public async Task<Sale> InsertSale(decimal totalPay, decimal subTotal, decimal totalDiscount, decimal taxes)
    {
        Sale sale = new Sale
        {
            TotalPay = totalPay,
            Subtotal = subTotal,
            TotalDiscount = totalDiscount,
            Taxes = taxes,
            Date = DateTime.Now
        };
        Sale result = await _command.InsertSale(sale);
        return result;
    }
    public async Task<List<SaleProductResponse>> InsertSaleProducts(Sale sale,
                                                            Dictionary<ProductResponse, int> productsAndQuantities)
    {
        List<SaleProductResponse> saleProductResponses = new List<SaleProductResponse>();
        foreach(KeyValuePair<ProductResponse, int> product in productsAndQuantities)
        {
            CreateSaleProductRequest saleProduct = new CreateSaleProductRequest
            {
                ProductId = product.Key.Id,
                SaleId = sale.SaleId,
                Quantity = product.Value,
                Price = product.Key.Price,
                Discount = product.Key.Discount
            };
            SaleProductResponse saleProductResponse = await _saleProductServices.CreateSaleProduct(saleProduct);
            saleProductResponses.Add(saleProductResponse);
        }
        return saleProductResponses;
    }
    public async Task<SaleResponse> GetSaleById(int id)
    {
        Sale sale = await _query.GetSaleById(id);
        List<SaleProductResponse> saleProductResponses = await _saleProductServices.GetSaleProductBySaleId(id);
        int totalQuantity = 0;
        foreach(SaleProductResponse saleProductResponse in saleProductResponses)
        {
            totalQuantity += saleProductResponse.Quantity;
        }
        return await CreateSaleResponse(sale, totalQuantity, saleProductResponses);
    }
    public async Task<List<SaleGetResponse>> GetListSales(DateTime? from, DateTime? to)
    {
        if(from > to)
        {
            throw new BadRequestException("La fecha desde no puede ser mayor a la fecha hasta");
        }
        List<Sale> sales = await _query.GetListSales(from, to);
        return await CreateSaleGetResponses(sales);
    }
    private async Task<SaleResponse> CreateSaleResponse(Sale sale, int totalQuantity,
                                                        List<SaleProductResponse> saleProductResponses)
    {
        SaleResponse response = new SaleResponse 
        {
            Id = sale.SaleId,
            TotalPay = sale.TotalPay,
            TotalQuantity = totalQuantity,
            SubTotal = sale.Subtotal,
            TotalDiscount = sale.TotalDiscount,
            Taxes = sale.Taxes,
            Date = sale.Date,
            Products = saleProductResponses 
        };
        return await Task.FromResult(response);
    } 
    private async Task<List<SaleGetResponse>> CreateSaleGetResponses(List<Sale> sales)
    {
        List<SaleGetResponse> saleGetResponses = new List<SaleGetResponse>();
        foreach(Sale sale in sales)
        {
            List<SaleProductResponse> saleProductResponses = await _saleProductServices
                                                            .GetSaleProductBySaleId(sale.SaleId);
            int totalQuantity = 0;
            foreach(SaleProductResponse saleProductResponse in saleProductResponses)
            {
                totalQuantity += saleProductResponse.Quantity;
            }
            SaleGetResponse saleGetResponse = new SaleGetResponse
            {
                Id = sale.SaleId,
                TotalPay = sale.TotalPay,
                TotalQuantity = totalQuantity,
                Date = sale.Date
            };
            saleGetResponses.Add(saleGetResponse);
        }
        return saleGetResponses;
    }             
}
