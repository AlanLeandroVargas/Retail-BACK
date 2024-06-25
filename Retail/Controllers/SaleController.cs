using Application.Exceptions;
using Application.Interfaces;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace Retail;
[Route("api/[controller]")]
[ApiController]
public class SaleController : ControllerBase
{
    private readonly ISaleServices _saleServices;

    public SaleController(ISaleServices saleServices)
    {
        _saleServices = saleServices;
    }

    
    [HttpPost]
    [ProducesResponseType(typeof(SaleResponse), 201)]
    [ProducesResponseType(typeof(ApiError), 400)]
    [ProducesResponseType(typeof(ApiError), 409)]    
    public async Task<IActionResult> CreateSale(SaleRequest request)
    {
        try
        {                        
            var result = await _saleServices.CreateSale(request);
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
    [ProducesResponseType(typeof(List<SaleGetResponse>), 200)]
    [ProducesResponseType(typeof(ApiError), 400)]
    public async Task<IActionResult> GetListSales(DateTime? from = null, DateTime? to = null)
    {
        try
        {
            var result = await _saleServices.GetListSales(from, to);
            return new JsonResult(result){StatusCode = 200};
        }
        catch (BadRequestException ex)
        {
            return new JsonResult(new ApiError{Message = ex.Message}){StatusCode = 400};
        }
    }
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SaleResponse), 200)]
    [ProducesResponseType(typeof(ApiError), 404)]    
    public async Task<IActionResult> GetSaleById(int id)
    {
        try
        {   
            var result = await _saleServices.GetSaleById(id);
            return new JsonResult(result){StatusCode = 200};
        }
        catch (NotFoundException ex)
        {
            return new JsonResult(new ApiError{Message = ex.Message}){StatusCode = 404};
        }
    }
}
