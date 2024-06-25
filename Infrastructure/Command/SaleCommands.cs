using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Command;

public class SaleCommands : ISaleCommands
{
    private readonly RetailContext _context;
    private readonly ISaleQuery _query;

    public SaleCommands(RetailContext context, ISaleQuery query)
    {
        _context = context;
        _query = query;
    }

    public async Task<Sale> InsertSale(Sale sale)
    {
        try
        {
            _context.Add(sale);
            await _context.SaveChangesAsync();
            return sale;
        }
        catch (DbUpdateException)
        {
            throw new Conflict("Error en la base de datos");
        }
    }
    public async Task<Sale> UpdateSale(UpdateSaleRequest request)
    {
        try
        {
            Sale sale = await _query.GetSaleById(request.SaleId);
            sale.TotalPay = request.TotalPay;
            sale.Subtotal = request.Subtotal;
            sale.TotalDiscount = request.TotalDiscount;
            sale.Taxes = request.Taxes;
            sale.Date = request.Date;
            await _context.SaveChangesAsync();
            return sale;
        }
        catch (DbUpdateException)
        {
            throw new Conflict("Error en la base de datos");
        }
    }

    public async Task DeleteSale(Sale sale)
    {
        try
        {
            _context.Remove(sale);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw new Conflict("Error en la base de datos");
        }
    }

    
}
