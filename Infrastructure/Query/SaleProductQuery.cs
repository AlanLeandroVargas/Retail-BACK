using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Query;

public class SaleProductQuery : ISaleProductQuery
{
    private readonly RetailContext _context;

    public SaleProductQuery(RetailContext context)
    {
        _context = context;
    }

    public async Task<List<SaleProduct>> GetListSaleProducts()
    {
        List<SaleProduct> saleproducts = await _context.SaleProducts
                                        .ToListAsync();
        return saleproducts;
    }

    public async Task<SaleProduct> GetSaleProductById(int shoppingCartId)
    {
        SaleProduct saleproduct = await _context.SaleProducts
                            .FirstOrDefaultAsync(s => s.ShoppingCartId == shoppingCartId);
        return saleproduct;
    }
    public async Task<List<SaleProduct>> GetSaleProductBySaleId(int id)
    {
        IQueryable<SaleProduct> saleProducts = _context.SaleProducts;
        saleProducts = saleProducts.Where(s => s.Sale == id);        
        return await saleProducts.ToListAsync();;
    }
    public async Task<SaleProduct?> SoldProduct(Guid productId)
    {
        SaleProduct? saleProduct = await _context.SaleProducts
                                .FirstOrDefaultAsync(spp => spp.Product == productId);
        return saleProduct;
    }
}
