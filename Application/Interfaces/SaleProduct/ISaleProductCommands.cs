using Application.Request;
using Domain.Entities;

namespace Application.Interfaces;

public interface ISaleProductCommands
{
    Task<SaleProduct> InsertSaleProduct(SaleProduct saleProduct);
    Task<SaleProduct> UpdateSaleProduct(UpdateSaleProductRequest request);
    Task DeleteSaleProduct(SaleProduct saleProduct);
}
