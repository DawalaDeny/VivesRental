using Microsoft.AspNetCore.Mvc;
using Vives.SDK;

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
	}
}
