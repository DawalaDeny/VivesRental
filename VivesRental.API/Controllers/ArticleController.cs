using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VivesRental.Enums;
using VivesRental.Services;
using VivesRental.Services.Abstractions;
using VivesRental.Services.Model.Requests;

namespace VivesRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {

        private readonly ArticleService articleService;


        public ArticleController(ArticleService articleService)
        {
            this.articleService = articleService;
        }

        //get all articles
        [HttpGet]
        public async Task<IActionResult> FindAsync()
        {
            var articles = await articleService.FindAsync();
            return Ok(articles);
        }

        //Get an article from its ID
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var article = await articleService.GetAsync(id);
            return Ok(article);
        }

        //Create a new article
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ArticleRequest request)
        {
            var article = await articleService.CreateAsync(request);
            return Ok(article);
        }

        //Update status of an article
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, ArticleStatus request)
        {
            var status = await articleService.UpdateStatusAsync(id, request);
            return Ok(status);
        }

        //Removes an article
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await articleService.RemoveAsync(id);
            return Ok();
        }

    }
}
