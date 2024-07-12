using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace Retail;
[Route("api/[controller]")]
[ApiController]

public class ProductController : ControllerBase
{
    private readonly IProductServices _productServices;

    public ProductController(IProductServices productServices)
    {
        _productServices = productServices;
    }
    [HttpPost]
    [ProducesResponseType(typeof(ProductResponse), 201)]
    [ProducesResponseType(typeof(ApiError), 400)]
    [ProducesResponseType(typeof(ApiError), 409)]    
    public async Task<IActionResult> CreateProduct(ProductRequest request)
    {
        try
        {
            var result = await _productServices.CreateProduct(request);
            return new JsonResult(result){StatusCode = 201};
        }
        catch (BadRequestException ex)
        {
            return new JsonResult(new ApiError{Message = ex.Message}){StatusCode = 400};
        }
        catch (Conflict ex)
        {
            return new JsonResult(new ApiError{Message = ex.Message}){StatusCode = 409};
        }
    }
    [HttpGet]
    [ProducesResponseType(typeof(List<ProductGetResponse>), 200)]
    [ProducesResponseType(typeof(ApiError), 400)]  
    public async Task<IActionResult> GetListProducts(string name = null, string category = null, int offset = 0, int limit = 0)
    {
        try
        {
            var result = await _productServices.GetListProducts(name, category, offset, limit);            
            return new JsonResult(result){StatusCode = 200};
        }
        catch (Exception)
        {
            throw;
        }
    }
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductResponse), 200)]
    [ProducesResponseType(typeof(ApiError), 404)]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        try
        {   
            var result = await _productServices.GetProductById(id);
            return new JsonResult(result){StatusCode = 200};
        }
        catch (NotFoundException ex)
        {
            return new JsonResult(new ApiError{Message = ex.Message}){StatusCode = 404};
        }
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ProductResponse), 200)]
    [ProducesResponseType(typeof(ApiError), 404)]
    [ProducesResponseType(typeof(ApiError), 409)]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        try{
            var result = await _productServices.DeleteProduct(id);
            return new JsonResult(result){StatusCode = 200};
        }
        catch (NotFoundException ex)
        {
            return new JsonResult(new ApiError{Message = ex.Message}){StatusCode = 404};
        }
        catch(Conflict ex)
        {
            return new JsonResult(new ApiError{Message = ex.Message}){StatusCode = 409};
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ProductResponse), 200)]
    [ProducesResponseType(typeof(ApiError), 400)]    
    [ProducesResponseType(typeof(ApiError), 404)]
    [ProducesResponseType(typeof(ApiError), 409)]
    public async Task<IActionResult> UpdateProduct(ProductRequest request, Guid id)
    {
        try
        {
            var result = await _productServices.UpdateProduct(request, id);
            return new JsonResult(result){StatusCode = 200};
        }
        catch (BadRequestException ex)
        {
            return new JsonResult(new ApiError{Message = ex.Message}){StatusCode = 400};
        }
        catch (Conflict ex)
        {
            return new JsonResult(new ApiError{Message = ex.Message}){StatusCode = 409};
        }
        catch (NotFoundException ex)
        {
            return new JsonResult(new ApiError{Message = ex.Message}){StatusCode = 404};
        }
    }
}
