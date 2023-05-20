using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VivesRental.Services;
using VivesRental.Services.Abstractions;
using VivesRental.Services.Model.Requests;
using VivesRental.UI.Models;

namespace VivesRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderLineController : ControllerBase
    {

        private readonly OrderLineService lineService;


        public OrderLineController(OrderLineService lineService)
        {
            this.lineService = lineService;
        }

        //Get all orderlines
        [HttpGet]
        public async Task<IActionResult> FindAsync()
        {
            var orderlines = await lineService.FindAsync();
            return Ok(orderlines);
        }

        //Get an orderline from its ID
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var orderline = await lineService.GetAsync(id);
            return Ok(orderline);
        }
        //Create orderline with 1 article?
        [HttpPost]
        public async Task<IActionResult> RentAsync(RentSingle a)
        {
            var articleId = a.articleId;
            var orderId = a.orderId;

            var orderline = await lineService.RentAsync(orderId, articleId);
            return Ok(orderline);
        }


        //Create orderline with * article?
        [HttpPost("*")]
        public async Task<IActionResult> RentAsync(Guid orderId, [FromForm] IList<Guid> articleIds)
        {
            var orderline = await lineService.RentAsync(orderId, articleIds);
            return Ok(orderline);
        }

        //Return rented article
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, DateTime returnedAt)
        {
            var orderline = await lineService.ReturnAsync(id, returnedAt);
            return Ok(orderline);
        }
    }
}
