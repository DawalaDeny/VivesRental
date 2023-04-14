using Microsoft.AspNetCore.Mvc;
using Vives.SDK;
using VivesRental.Model;

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
        public async Task<IActionResult> add(Customer customer)
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
