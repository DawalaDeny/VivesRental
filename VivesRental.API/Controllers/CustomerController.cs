using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VivesRental.Model;
using VivesRental.Services;
using VivesRental.Services.Model.Requests;

namespace VivesRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
    private readonly CustomerService customerService;

    
    public CustomerController(CustomerService customerService)
    {
        this.customerService = customerService;
    }

    //Get all customers
    [HttpGet]
    public async Task <IActionResult> FindAsync()
    {
        var customers = await customerService.FindAsync();
        return Ok(customers);
    }

    //Get a single customer with ID
    [HttpGet("{id:Guid}")]
    public async Task <IActionResult> GetAsync(Guid id)
    {
        var customers = await customerService.GetAsync(id);
        return Ok(customers);
    }
    
    //Create new customer
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CustomerRequest request)
    {
        var customer = await customerService.CreateAsync(request);
        return Ok(customer);
    }

    //Edit a customer
    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] CustomerRequest request)
    {
        var customer = await customerService.EditAsync(id, request);
        return Ok(customer);
    }
    
    //Removes a customer
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        await customerService.RemoveAsync(id);
        return Ok();
    }
    }
}
