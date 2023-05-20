using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Vives.Models;
using Vives.SDK;
using VivesRental.UI.Models;
using VivesRental.UI.SDK;

namespace Vives.Controllers
{
	public class HomeController : Controller
	{
        private readonly OrderSdk ordSdk;
        private readonly CustomerManagementSdk cusSdk;
        private readonly ProductManagementSdk proSdk;
        private readonly ArticleSdk artSdk;
        private readonly ReservationSdk resSdk;

        public HomeController(OrderSdk ordSdk, CustomerManagementSdk cusSdk, ProductManagementSdk proSdk, ArticleSdk artSdk, ReservationSdk resSdk)
        {
			this.ordSdk = ordSdk;
            this.cusSdk = cusSdk;
			this.proSdk = proSdk;
			this.artSdk = artSdk;
			this.resSdk = resSdk;
        }

		public async Task<IActionResult> Index()
        {
            var listCustomers = await cusSdk.FindAsync();
			var listProducts = await proSdk.FindAsync();
			var listArticles = await artSdk.FindAsync();
			var listOrders = await ordSdk.FindAsync();
			var listReservations = await resSdk.FindAsync();

            var alles = new IndexPaginaDetails();
            alles.Customers = listCustomers;
            alles.Articles = listArticles;
			alles.Orders = listOrders;
			alles.Reservations = listReservations;
            alles.Products = listProducts;
			
            return View(alles);
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}