using Domain.Entities;

namespace Application.Interfaces;

public interface ISaleProductQuery
{
    Task<List<SaleProduct>> GetListSaleProducts();
    Task<SaleProduct> GetSaleProductById(int shoppingCartId);
    Task<List<SaleProduct>> GetSaleProductBySaleId(int id);
    Task<SaleProduct> SoldProduct(Guid productId);
}
