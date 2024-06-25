using Application.Interfaces;
using Application.Request;
using Application.Response;
using Domain.Entities;

namespace Application.UseCase;

public class CategoryServices : ICategoryServices
{
    private readonly ICategoryCommands _command;
    private readonly ICategoryQuery _query;

    public CategoryServices(ICategoryCommands command, ICategoryQuery query)
    {
        _command = command;
        _query = query;
    }

    public async Task<CategoryResponse> CreateCategory(CreateCategoryRequest request)
    {
        var category = new Category 
        {
            Name = request.Name
        };
        Category result = await _command.InsertCategory(category);
        return await CreateCategoryResponse(result);
    }
    public async Task<CategoryResponse> UpdateCategory(UpdateCategoryRequest request)
    {
        Category category = await _command.UpdateCategory(request);
        return await CreateCategoryResponse(category);
    }

    public async Task DeleteCategory(int id)
    {
        await _command.DeleteCategory(id);
    }

    public async Task<List<Category>> GetListCategories()
    {
        List<Category> categories = await _query.GetListCategories();
        return categories;
    } 

    public async Task<Category> GetCategoryById(int categoryId)
    {
        Category categories = await _query.GetCategoryById(categoryId);
        return categories;
    }
    private Task<CategoryResponse> CreateCategoryResponse(Category category)
    {
        CategoryResponse response = new CategoryResponse
        {
            Id = category.CategoryId,
            Name = category.Name
        };
        return Task.FromResult(response);
    }
}
