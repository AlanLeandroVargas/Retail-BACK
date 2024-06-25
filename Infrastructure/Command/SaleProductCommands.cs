using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Command;

public class SaleProductCommands : ISaleProductCommands
{
    private readonly RetailContext _context;
    private readonly ISaleProductQuery _query;

    public SaleProductCommands(RetailContext context, ISaleProductQuery query)
    {
        _context = context;
        _query = query;
    }

    public async Task<SaleProduct> InsertSaleProduct(SaleProduct saleProduct)
    {
        try
        {
            _context.Add(saleProduct);
            await _context.SaveChangesAsync();
            return saleProduct;
        }
        catch (DbUpdateException)
        {
            throw new Conflict("Error en la base de datos");
        }
    }

    public async Task DeleteSaleProduct(SaleProduct saleProduct)
    {
        try
        {
            _context.Remove(saleProduct);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw new Conflict("Error en la base de datos");
        }
    }

    public async Task<SaleProduct> UpdateSaleProduct(UpdateSaleProductRequest request)
    {
        try
        {
            SaleProduct saleProduct = await _query.GetSaleProductById(request.ShoppingCartId);
            saleProduct.Product = request.ProductId;
            saleProduct.Sale = request.SaleId;
            saleProduct.Quantity = request.Quantity;
            saleProduct.Price = request.Price;
            saleProduct.Discount = request.Discount;
            await _context.SaveChangesAsync();
            return saleProduct;
        }        
        catch (DbUpdateException)
        {
            throw new Conflict("Error en la base de datos");
        }
    }
}
