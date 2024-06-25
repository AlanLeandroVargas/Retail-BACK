using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Application.Response;
using Domain.Entities;

namespace Application.UseCase;

public class ProductServices : IProductServices
{
    private readonly IProductCommands _command;
    private readonly IProductQuery _query;
    private readonly ICategoryServices _categoryServices;
    private readonly ISaleProductServices _saleProductServices;

    public ProductServices(IProductCommands command, IProductQuery query, ICategoryServices categoryServices,
                            ISaleProductServices saleProductServices)
    {
        _command = command;
        _query = query;
        _categoryServices = categoryServices;
        _saleProductServices = saleProductServices;
    }

    public async Task<ProductResponse> CreateProduct(ProductRequest request)
    {
        await CheckIfCategoryExists(request);
        if(request.Discount > 100 || request.Discount < 0)
        {
            throw new BadRequestException("Descuento invalido");
        }
        Product product = new Product 
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Category = request.Category,
            Discount = request.Discount,
            ImageUrl = request.ImageUrl
        };
        Product result = await _command.InsertProduct(product);
        return await CreateProductResponse(result);
    }

    private async Task<ProductResponse> CreateProductResponse(Product product)
    {
        Category category = await _categoryServices.GetCategoryById(product.Category);
        ProductResponse productResponse = new ProductResponse
        {
            Id = product.ProductId,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Category = new CategoryResponse {Id = category.CategoryId, Name = category.Name},
            Discount = product.Discount,
            ImageUrl = product.ImageUrl
        };
        return await Task.FromResult(productResponse);
    }
    public async Task<ProductResponse> UpdateProduct(ProductRequest request, Guid id)
    {
        await CheckIfCategoryExists(request);
        if(request.Discount > 100 || request.Discount < 0)
        {
            throw new BadRequestException("Descuento invalido");
        }
        Product product = await _command.UpdateProduct(request, id);
        return await CreateProductResponse(product);
    }

    public async Task<ProductResponse> DeleteProduct(Guid id)
    {
        Product product = await _query.GetProductById(id);
        if(await _saleProductServices.IsProductSold(id))
        {
            throw new Conflict("No se puede eliminar un producto vendido");
        } 
        ProductResponse productResponse = await CreateProductResponse(product);
        await _command.DeleteProduct(product);
        return productResponse;
    }

    public async Task<List<ProductGetResponse>> GetListProducts(string name, string category, int offset, int limit)
    {
        List<Product> products = await _query.GetListProducts(name, category, offset, limit);
        return await CreateListProductGetResponse(products);
    }

    public async Task<ProductResponse> GetProductById(Guid productId)
    {
        Product product = await _query.GetProductById(productId);
        return await CreateProductResponse(product);
    }
    private async Task<List<ProductGetResponse>> CreateListProductGetResponse(List<Product> products)
    {
        List<ProductGetResponse> productGetResponses = new List<ProductGetResponse>();
        foreach(Product product in products)
        {
            Category category = await _categoryServices.GetCategoryById(product.Category);
            ProductGetResponse response = new ProductGetResponse
            {
                Id = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Discount = product.Discount,
                ImageUrl = product.ImageUrl,  
                CategoryName = category.Name
            };
            productGetResponses.Add(response);
        }
        
        return await Task.FromResult(productGetResponses);
    }
    private async Task CheckIfCategoryExists(ProductRequest request)
    {
        try
        {
            Category category = await _categoryServices.GetCategoryById(request.Category);  
        }
        catch (NotFoundException)
        {
            throw new BadRequestException("La categoria ingresada no es valida");
        }
    }
}