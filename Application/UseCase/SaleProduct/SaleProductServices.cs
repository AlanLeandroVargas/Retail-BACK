using Application.Interfaces;
using Application.Request;
using Application.Response;
using Domain.Entities;

namespace Application.UseCase;

public class SaleProductServices : ISaleProductServices
{
    private readonly ISaleProductCommands _command;
    private readonly ISaleProductQuery _query;

    public SaleProductServices(ISaleProductCommands command, ISaleProductQuery query)
    {
        _command = command;
        _query = query;
    }

    public async Task<SaleProductResponse> CreateSaleProduct(CreateSaleProductRequest request)
    {
        SaleProduct saleProduct = new SaleProduct 
        {
            Product = request.ProductId,
            Sale = request.SaleId,
            Quantity = request.Quantity,
            Price = request.Price,
            Discount = request.Discount
        };
        SaleProduct result = await _command.InsertSaleProduct(saleProduct);
        return await CreateSaleProductResponse(result);
    }    
    public async Task<SaleProductResponse> UpdateSaleProduct(UpdateSaleProductRequest request)
    {
        SaleProduct saleProduct = await _command.UpdateSaleProduct(request);
        return await CreateSaleProductResponse(saleProduct);
    }

    public async Task DeleteSaleProduct(int id)
    {   
        SaleProduct saleProduct = await _query.GetSaleProductById(id);
        await _command.DeleteSaleProduct(saleProduct);
    }

    public async Task<List<SaleProduct>> GetListSaleProducts()
    {
        List<SaleProduct> saleProducts = await _query.GetListSaleProducts();
        return saleProducts;
    }

    public async Task<SaleProduct> GetSaleProductById(int shoppingCartId)
    {
        SaleProduct saleProduct = await _query.GetSaleProductById(shoppingCartId);
        return saleProduct;
    }
    public async Task<List<SaleProductResponse>> GetSaleProductBySaleId(int id)
    {
        List<SaleProduct> saleProducts = await _query.GetSaleProductBySaleId(id);
        List<SaleProductResponse> saleProductResponses = await CreateSaleProductResponses(saleProducts);
        return saleProductResponses;
    }
    private Task<SaleProductResponse> CreateSaleProductResponse(SaleProduct saleProduct)
    {
        SaleProductResponse response = new SaleProductResponse 
        {
            Id = saleProduct.ShoppingCartId,
            ProductId = saleProduct.Product,
            Quantity = saleProduct.Quantity,
            Price = saleProduct.Price,
            Discount = saleProduct.Discount
        };
        return Task.FromResult(response);
    }
    private Task<List<SaleProductResponse>> CreateSaleProductResponses(List<SaleProduct> saleProducts)
    {
        List<SaleProductResponse> saleProductResponses = new List<SaleProductResponse>();
        foreach(SaleProduct saleProduct in saleProducts)
        {
            SaleProductResponse response = new SaleProductResponse 
            {
                Id = saleProduct.ShoppingCartId,
                ProductId = saleProduct.Product,
                Quantity = saleProduct.Quantity,
                Price = saleProduct.Price,
                Discount = saleProduct.Discount
            };
            saleProductResponses.Add(response);
        }
        
        return Task.FromResult(saleProductResponses);
    }
    public async Task<bool> IsProductSold(Guid productId)
    {
        SaleProduct saleProduct = await _query.SoldProduct(productId);
        if(saleProduct == null)
        {
            return await Task.FromResult(false);
        }
        return await Task.FromResult(true);
    }
}
