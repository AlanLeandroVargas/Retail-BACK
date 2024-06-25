using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Query;

public class SaleQuery : ISaleQuery
{
    private readonly RetailContext _context;

    public SaleQuery(RetailContext context)
    {
        _context = context;
    }

    public async Task<List<Sale>> GetListSales(DateTime? from, DateTime? to)
    {
        IQueryable<Sale> sales = _context.Sales;
        if(from != null)
        {
            sales = sales.Where(s => s.Date >= from);
        }
        if(to != null)
        {
            sales = sales.Where(s => s.Date <= to);
        }
        return await sales.ToListAsync();
    }

    public async Task<Sale> GetSaleById(int saleId)
    {
        Sale? sale = await _context.Sales
                    .FirstOrDefaultAsync(s => s.SaleId == saleId);
        if(sale == null) throw new NotFoundException("Venta no encontrada");
        return sale;
    }
}
