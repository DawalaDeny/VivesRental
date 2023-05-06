using Microsoft.AspNetCore.Mvc;
using Vives.SDK;
using VivesRental.Services.Model.Requests;

namespace Vives.Controllers
{
	public class ProductController : Controller
	{
		private readonly ProductManagementSdk Sdk;

		public ProductController(ProductManagementSdk Sdk)
		{
			this.Sdk = Sdk;
		}
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var products = await Sdk.Find();

			return View(products);
		}
        public IActionResult addProduct()
        {
            return View("form");
        }

        [HttpPost]
        public async Task<IActionResult> add(ProductRequest product)
        {
            if (!ModelState.IsValid)
            {
                return View("form", product);
            }

            await Sdk.Create(product);

            return RedirectToAction("Index");
        }
    }
}
