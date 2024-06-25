using Application.Request;
using Application.Response;
using Domain.Entities;

namespace Application.Interfaces;

public interface ICategoryServices
{
    Task<CategoryResponse> CreateCategory(CreateCategoryRequest request);
    Task<CategoryResponse> UpdateCategory(UpdateCategoryRequest request);
    Task DeleteCategory(int id);
    Task<List<Category>> GetListCategories();
    Task<Category> GetCategoryById(int categoryId);
}
