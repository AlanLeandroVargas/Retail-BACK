using Application.Request;
using Domain.Entities;

namespace Application.Interfaces;

public interface ICategoryCommands
{
    Task<Category> InsertCategory(Category category);
    Task<Category> UpdateCategory(UpdateCategoryRequest request);
    Task DeleteCategory(int id);
}
