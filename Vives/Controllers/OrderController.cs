using Microsoft.AspNetCore.Mvc;
using Vives.SDK;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;
using VivesRental.UI.SDK;


namespace VivesRental.UI.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderSdk Sdk;
        private readonly CustomerManagementSdk custSdk;


        public OrderController(OrderSdk Sdk, CustomerManagementSdk custSdk)
        {
            this.Sdk = Sdk;
            this.custSdk = custSdk;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = await Sdk.FindAsync();


            return View(orders);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var customers = await custSdk.FindAsync();
            ViewData["Customers"] = customers;
            return View();
            
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid customerID)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("index");
            }

            await Sdk.CreateAsync(customerID);
            
            return RedirectToAction("Index");
        }
    }
}