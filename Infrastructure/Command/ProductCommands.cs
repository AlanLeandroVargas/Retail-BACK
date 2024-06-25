using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Command;

public class ProductCommands : IProductCommands
{
    private readonly RetailContext _context;
    private readonly IProductQuery _query;

    public ProductCommands(RetailContext context, IProductQuery query)
    {
        _context = context;
        _query = query;
    }

    public async Task<Product> InsertProduct(Product product)
    {
        try
        {
            CheckIfProductExists(product.Name);          
            _context.Add(product);
            await _context.SaveChangesAsync();        
            return product;
        }        
        catch (DbUpdateException)
        {
            throw new Conflict("Error en la base de datos");
        }
    }
    public async Task<Product> UpdateProduct(ProductRequest request, Guid id)
    {
        try 
        {            
            Product product = await _query.GetProductById(id);
            if(product.Name != request.Name) 
            {
                CheckIfProductExists(request.Name);
            }
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Category = request.Category;
            product.Discount = request.Discount;
            product.ImageUrl = request.ImageUrl;
            await _context.SaveChangesAsync();
            return product;
        }
        catch (DbUpdateException)
        {
            throw new Conflict("Error en la base de datos");
        }
    }

    public async Task DeleteProduct(Product product)
    {
        try
        {               
            _context.Remove(product);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw new Conflict("Error en la base de datos");
        }
    }
    private Conflict CheckIfProductExists(string productName)
    {
        if(_context.Products.Any(p => p.Name.ToLower() == productName.ToLower()))
            {
                throw new Conflict("Ya existe un producto con ese nombre");
            }
        return null;
    }
}
