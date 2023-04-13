using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VivesRental.Services;
using VivesRental.Services.Abstractions;
using VivesRental.Services.Model.Requests;

namespace VivesRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService orderService;

        public OrderController(OrderService orderService)

        {
            this.orderService = orderService;
        }

        //get all orders
        [HttpGet]
        public async Task<IActionResult> FindAsync()
        {
            var orders = await orderService.FindAsync();
            return Ok(orders);
        }

        //Get all orders with order ID
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var orders = await orderService.GetAsync(id);
            return Ok(orders);
        }

        //Create new order
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Guid customerId)
        {
            var order = await orderService.CreateAsync(customerId);
            return Ok(order);
        }

        //Change dates of orderlines because it's returned
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> ReturnAsync([FromRoute] Guid id, DateTime returnedAt)
        {
            var order = await orderService.ReturnAsync(id, returnedAt);
            return Ok(order);
        }
    }
}
