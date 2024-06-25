using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Command;

public class CategoryCommands : ICategoryCommands
{
    private readonly RetailContext _context;
    private readonly ICategoryQuery _query;

    public CategoryCommands(RetailContext context, ICategoryQuery query)
    {
        _context = context;
        _query = query;
    }

    public async Task<Category> InsertCategory(Category category)
    {
        try
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }
        catch (DbUpdateException)
        {
            throw new Conflict("Error en la base de datos");
        }
                
    }
    public async Task<Category> UpdateCategory(UpdateCategoryRequest request)
    {
        try
        {
            Category category = await _query.GetCategoryById(request.CategoryId);
            category.Name = request.Name;
            await _context.SaveChangesAsync();
            return category;
        }
        catch (DbUpdateException)
        {
            throw new Conflict("Error en la base de datos");
        }
        
    }

    public async Task DeleteCategory(int id)
    {
        try
        {
            Category category = await _query.GetCategoryById(id);
            _context.Remove(category);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw new Conflict("Error en la base de datos");
        }
        
    }
}
