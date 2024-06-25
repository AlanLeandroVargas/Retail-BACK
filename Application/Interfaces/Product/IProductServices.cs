using Application.Request;
using Application.Response;

namespace Application.Interfaces;

public interface IProductServices
{
    Task<ProductResponse> CreateProduct(ProductRequest request);
    Task<ProductResponse> UpdateProduct(ProductRequest request, Guid id);
    Task<ProductResponse> DeleteProduct(Guid id);
    Task<List<ProductGetResponse>> GetListProducts(string name, string category, int offset, int limit);
    Task<ProductResponse> GetProductById(Guid productId);

}
