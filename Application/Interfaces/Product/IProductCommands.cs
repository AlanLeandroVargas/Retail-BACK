using Application.Request;
using Domain.Entities;

namespace Application.Interfaces;

public interface IProductCommands
{
    Task<Product> InsertProduct(Product product);
    Task<Product> UpdateProduct(ProductRequest request, Guid id);
    Task DeleteProduct(Product product);
}
