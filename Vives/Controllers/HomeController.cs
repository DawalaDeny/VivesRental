using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Vives.Models;
using Vives.SDK;
using VivesRental.Enums;
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
        private readonly OrderLineSdk orderLineSdk;

        public HomeController(OrderSdk ordSdk, CustomerManagementSdk cusSdk, ProductManagementSdk proSdk, ArticleSdk artSdk, ReservationSdk resSdk, OrderLineSdk orderLineSdk)
        {
			this.ordSdk = ordSdk;
            this.cusSdk = cusSdk;
			this.proSdk = proSdk;
			this.artSdk = artSdk;
			this.resSdk = resSdk;
            this.orderLineSdk = orderLineSdk;

        }

		public async Task<IActionResult> Index()
        {
            var listCustomers = await cusSdk.FindAsync();
			var listProducts = await proSdk.FindAsync();
			var listArticles = await artSdk.FindAsync();
			var listOrders = await ordSdk.FindAsync();
			var listReservations = await resSdk.FindAsync();
            var listOrderlines = await orderLineSdk.FindAsync();

            var alles = new IndexPaginaDetails();
            alles.Customers = listCustomers;
            alles.Articles = listArticles;
			alles.Orders = listOrders;
			alles.Reservations = listReservations;
            alles.Products = listProducts;
            alles.Orderlines = listOrderlines;
			
            return View(alles);
		}
        public async Task<IActionResult> Rent()
        {
            
            var listArticles = await artSdk.FindAsync();
            var listProducts = await proSdk.FindAsync();

            var productsNormaalAanwezig = listProducts.Where(product => listArticles.Any(article => article.ProductId == product.Id && article.Status == ArticleStatus.Normal)).ToList();



            return View(productsNormaalAanwezig);
        }
        [HttpGet]
        public async Task<IActionResult> RentSingle()
        {

            var articles = await artSdk.FindAsync();
            //selecteren alle artikelen met status normal, duplicaten (productnames) wegdoen, 
            var normalArticles = articles.Where(a => a.Status == ArticleStatus.Normal).DistinctBy(a => a.ProductName).ToList();
            ViewData["articles"] = normalArticles;

            var customers = await cusSdk.FindAsync();
            ViewData["customers"] = customers;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RentSingle(Guid articleId, Guid customerId)
        {
            var orderResult = await ordSdk.CreateAsync(customerId);
            await orderLineSdk.RentAsync(orderResult.Id, articleId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> RentMultiple()
        {

            var articles = await artSdk.FindAsync();
        
            var normalArticles = articles.Where(a => a.Status == ArticleStatus.Normal).ToList();
            ViewData["articles"] = normalArticles;

            var customers = await cusSdk.FindAsync();
            ViewData["customers"] = customers;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RentMultiple(IList<Guid> articleIds, Guid customerId)
        {
            var orderResult = await ordSdk.CreateAsync(customerId);
            await orderLineSdk.RentMultipleAsync(orderResult.Id, articleIds);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}