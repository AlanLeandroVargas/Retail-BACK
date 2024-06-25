using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Query;

public class CategoryQuery : ICategoryQuery
{
    private readonly RetailContext _context;

    public CategoryQuery(RetailContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetListCategories()
    {
        List<Category> categories = await _context.Categories
                        .ToListAsync();
        return categories;
    }

    public async Task<Category> GetCategoryById(int categoryId)
    {
        Category? category = await _context.Categories.FirstOrDefaultAsync(s => s.CategoryId == categoryId);
        if(category == null) throw new NotFoundException("Categoria no encontrada");
        return category;
    }

    
}
