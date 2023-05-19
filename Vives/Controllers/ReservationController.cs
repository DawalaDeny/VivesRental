using Microsoft.AspNetCore.Mvc;
using Vives.SDK;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;
using VivesRental.UI.SDK;

namespace VivesRental.UI.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ReservationSdk Sdk;
        private readonly CustomerManagementSdk custSdk;
        private readonly ArticleSdk artSdk;

        public ReservationController(ReservationSdk sdk, CustomerManagementSdk custSdk, ArticleSdk artSdk)
        {
            Sdk = sdk;
            this.custSdk = custSdk;
            this.artSdk = artSdk;
        }
        public async Task<IActionResult> Index()
        {
            var reservations = await Sdk.FindAsync();
            return View(reservations);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var customers = await custSdk.FindAsync();
            ViewData["Customers"] = customers;
            var articles = await artSdk.FindAsync();
            ViewData["Articles"] = articles;

            var reservation = new ArticleReservationResult
            {
                FromDateTime = DateTime.Now,
                UntilDateTime = DateTime.Now.AddDays(7),
        };

            return View(reservation);
           
           

        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleReservationRequest articleReservation)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("index");
            }

            await Sdk.CreateAsync(articleReservation);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var reservation = await Sdk.GetAsync(id);

            var name = reservation.ProductName;
            ViewBag.Name = name;
            var customer = reservation.CustomerFirstName + " " +  reservation.CustomerLastName;
            ViewBag.Customer = customer;

            if (reservation is null)
            {
                return RedirectToAction("Index");
            }

            return View(reservation);
        }
        [HttpPost("[controller]/Delete/{id:Guid?}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {

            await Sdk.DeleteAsync(id);
            return RedirectToAction("Index");

        }
    }


}
