using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VivesRental.Services;
using VivesRental.Services.Abstractions;
using VivesRental.Services.Model.Requests;

namespace VivesRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleReservationController : ControllerBase
    {
        private readonly ArticleReservationService reservationService;


        public ArticleReservationController(ArticleReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        //Get all reservations
        [HttpGet]
        public async Task<IActionResult> FindAsync()
        {
            var reservations = await reservationService.FindAsync();
            return Ok(reservations);
        }

        
        //Get a reservation from its ID
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var reservation = await reservationService.GetAsync(id);
            return Ok(reservation);
        }

        //Create a new reservation
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ArticleReservationRequest request)
        {
            var reservation = await reservationService.CreateAsync(request);
            return Ok(reservation);
        }

        //Remove a reservation
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id)
        {
            var reservation = await reservationService.RemoveAsync(id);
            return Ok(reservation);
        }
    }
}
