using Microsoft.AspNetCore.Mvc;
using Vives.SDK;
using VivesRental.Model;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace Vives.Controllers
{
    public class CustomerManagement : Controller
    {
        private readonly CustomerManagementSdk Sdk;

        public CustomerManagement(CustomerManagementSdk Sdk)
        {
            this.Sdk = Sdk;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var customers = await Sdk.Find();

            return View(customers);
        }
      
        public IActionResult addCustomer()
        {
            return View("Form");
        }

        [HttpPost]
        public async Task<IActionResult> add(CustomerRequest customer)
        {
            if (!ModelState.IsValid)
            {
                return View("Form",customer);
            }
            
            await Sdk.Create(customer);

            return RedirectToAction("Index");
        }
    }
}
