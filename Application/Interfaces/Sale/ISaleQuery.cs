using Domain.Entities;

namespace Application.Interfaces;

public interface ISaleQuery
{
    Task<List<Sale>> GetListSales(DateTime? from, DateTime? to);
    Task<Sale> GetSaleById(int saleId);
}
