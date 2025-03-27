using CRM.Application.DTOs;
using CRM.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _customerService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var customer = await _customerService.GetByIdAsync(id);
        return customer == null ? NotFound() : Ok(customer);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> Filter([FromQuery] string? name, [FromQuery] string? region, [FromQuery] DateTime? date)
    {
        var result = await _customerService.FilterAsync(name, region, date);
        return Ok(result);
    }

    [HttpPost]    
    public async Task<IActionResult> Create([FromBody] CustomerDto dto)
    {
        await _customerService.AddAsync(dto);
        return Ok();
    }

    [HttpPut]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromBody] CustomerDto dto)
    {
        await _customerService.UpdateAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _customerService.DeleteAsync(id);
        return Ok();
    }
}
