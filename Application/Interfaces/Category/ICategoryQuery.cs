using Domain.Entities;

namespace Application.Interfaces;

public interface ICategoryQuery
{
    Task<List<Category>> GetListCategories();
    Task<Category> GetCategoryById(int categoryId);
}
