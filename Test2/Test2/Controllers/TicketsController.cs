using Microsoft.AspNetCore.Mvc;
using Test2.Dto;
using Test2.Services;

namespace Test2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly IDbService _dbService;

    public TicketsController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPurchaseDetails(int id, CancellationToken cancellationToken)
    {
        return Ok(await _dbService.GetCustomerPurchaseAsync(id, cancellationToken));
    }

    [HttpPost("/customers")]
    public async Task<IActionResult> AddCustomerPurchases([FromBody] CustomerAddDto customerAddDto, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }
        var result = await _dbService.AddCustomer(customerAddDto, cancellationToken);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        return Created();
        
    }
}