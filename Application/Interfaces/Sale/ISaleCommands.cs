using Application.Request;
using Domain.Entities;

namespace Application.Interfaces;

public interface ISaleCommands
{
    Task<Sale> InsertSale(Sale sale);
    Task<Sale> UpdateSale(UpdateSaleRequest request);
    Task DeleteSale(Sale sale);
}
