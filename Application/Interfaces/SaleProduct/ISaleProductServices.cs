using Application.Request;
using Application.Response;
using Domain.Entities;

namespace Application.Interfaces;

public interface ISaleProductServices
{
    Task<SaleProductResponse> CreateSaleProduct(CreateSaleProductRequest request);
    Task<SaleProductResponse> UpdateSaleProduct(UpdateSaleProductRequest request);
    Task DeleteSaleProduct(int id);
    Task<List<SaleProduct>> GetListSaleProducts();
    Task<SaleProduct> GetSaleProductById(int shoppingCartId);
    Task<List<SaleProductResponse>> GetSaleProductBySaleId(int id);
    Task<bool> IsProductSold(Guid productId);
}
