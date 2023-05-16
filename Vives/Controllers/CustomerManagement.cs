using Microsoft.AspNetCore.Mvc;
using Vives.SDK;
using VivesRental.Services.Model.Requests;


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
      
        public IActionResult AddCustomer()
        {
            return View("Form");
        }

        [HttpPost]
        public async Task<IActionResult> Add(CustomerRequest customer)
        {
            if (!ModelState.IsValid)
            {
                return View("Form",customer);
            }
            
            await Sdk.CreateAsync(customer);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var customer = await Sdk.GetAsync(id);

            if (customer is null)
            {
                return RedirectToAction("Index");
            }

            return View(customer);
        }
        [HttpPost("[controller]/Delete/{id:Guid?}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await Sdk.DeleteAsync(id);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> EditPage(Guid id)
        {
            var customer = await Sdk.GetAsync(id);
            if (customer is null)
            {
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> EditPage(Guid id, CustomerRequest request)
        {
            var customer = await Sdk.GetAsync(id);
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            await Sdk.UpdateAsync(id, request);

            return RedirectToAction("Index");
        }
    }
}
