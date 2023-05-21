using Microsoft.AspNetCore.Mvc;
using Vives.SDK;
using VivesRental.Services.Model.Requests;
using VivesRental.UI.Models;
using VivesRental.UI.SDK;

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
            var products = await Sdk.FindAsync();


            return View(products);
        }

        public IActionResult AddProduct()
        {
            return View("form");
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductRequest product)
        {
            if (!ModelState.IsValid)
            {
                return View("form", product);
            }

            await Sdk.CreateAsync(product);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await Sdk.GetAsync(id);

            if (product is null)
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [HttpPost("[controller]/Delete/{id:Guid?}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await Sdk.DeleteAsync(id);
            }
            catch (Exception ex)
            {

                return View("ErrorPage", ex);

            }
            
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> EditPage(Guid id)
        {
            var product = await Sdk.GetAsync(id);
            if (product is null)
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditPage(Guid id, ProductRequest request)
        {
            var product = await Sdk.GetAsync(id);
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            await Sdk.UpdateAsync(id, request);
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddArticles(Guid id)
        {
            var artikelsMakenVm = new ArtikelsMakenVM();
            artikelsMakenVm.id = id;
            return View(artikelsMakenVm);
        }
        [HttpGet]
        
        [HttpPost]
        public async Task<IActionResult> AddArticlesAdd(ArtikelsMakenVM artikelsMakenVm)
        {
            var id = artikelsMakenVm.id;
            var amount = artikelsMakenVm.amount;
            
            await Sdk.GenerateArticlesAsync(id, amount);
            
            return RedirectToAction("Index");
        }
    }
}
