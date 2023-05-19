using Microsoft.AspNetCore.Mvc;
using Vives.SDK;
using VivesRental.Services.Model.Requests;
using VivesRental.UI.SDK;

namespace VivesRental.UI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ArticleSdk artSdk;
        private readonly ProductManagementSdk prodSdk;

        public ArticleController(ArticleSdk asdk, ProductManagementSdk pskd)
        {
            artSdk = asdk;
            prodSdk = pskd;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var articles = await artSdk.FindAsync();

            return View(articles);
        }
        [HttpGet]
        public async Task<IActionResult> Product(Guid productId)
        {
            var articles = await artSdk.FindAsync();
            var productArticles = articles.Where(a => a.ProductId == productId).ToList();
            var products = await prodSdk.FindAsync();
            var productName = products.FirstOrDefault(a => a.Id == productId)?.Name;

            ViewBag.ProductName = productName;
            return View(productArticles);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            
            var products = await prodSdk.FindAsync();
            ViewData["products"] = products;
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(ArticleRequest article)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create");
            }

            await artSdk.CreateAsync(article);
            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var article = await artSdk.GetAsync(id);
            if (article is null)
            {
                return RedirectToAction("Index");
            }
            
            return View(article);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ArticleRequest request)
        {
            var article = await artSdk.GetAsync(id);
            if (!ModelState.IsValid)
            {
                return View(article);
            }

            await artSdk.UpdateAsync(id, request);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var article = await artSdk.GetAsync(id);
            
            var name = article.ProductName;
            ViewBag.Name = name;

            if (article is null)
            {
                return RedirectToAction("Index");
            }

            return View(article);
        }
        [HttpPost("[controller]/Delete/{id:Guid?}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {

            await artSdk.DeleteAsync(id);
            return RedirectToAction("Index");

        }
    }
}

