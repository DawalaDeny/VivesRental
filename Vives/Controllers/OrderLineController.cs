using Microsoft.AspNetCore.Mvc;
using Vives.SDK;
using VivesRental.Enums;
using VivesRental.UI.SDK;

namespace VivesRental.UI.Controllers
{
    public class OrderLineController : Controller
    {
        private readonly OrderLineSdk Sdk;
        private readonly ArticleSdk artSdk;
        private readonly OrderSdk ordSdk;


        public OrderLineController(OrderLineSdk Sdk, ArticleSdk artSdk, OrderSdk ordSdk)
        {
            this.Sdk = Sdk;
            this.artSdk = artSdk;
            this.ordSdk = ordSdk;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orderline = await Sdk.FindAsync();


            return View(orderline);
        }

        [HttpGet]
        public async Task<IActionResult> RentSingle()
        {

            var articles = await artSdk.FindAsync();
            var normalArticles = articles.Where(a => a.Status == ArticleStatus.Normal).ToList(); ;
            ViewData["articles"] = normalArticles;

            var orders = await ordSdk.FindAsync();
            var avaibleOrders = orders.Where(a => a.ReturnedAt == null).ToList();
            ViewData["Orders"] = avaibleOrders;
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RentSingle(Guid articleId, Guid orderId)
        {

            await Sdk.RentAsync( orderId, articleId);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Return(Guid id)
        {

            var orderline = await Sdk.GetAsync(id);
            return View(orderline);
        }
        [HttpPost("[controller]/Return/{id:Guid?}")]
        public async Task<IActionResult> ReturnOk(Guid id)
        {
            await Sdk.ReturnAsync(id);
            return RedirectToAction("Index");
        }
    }
}
