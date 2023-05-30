using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using VivesBlog.Ui.Mvc.Core;
using VivesBlog.Ui.Mvc.Models;

namespace VivesBlog.Ui.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly VivesBlogDbContext _dbContext;

        public HomeController(VivesBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var articles = _dbContext.Articles
                .Include(a => a.Author)
                .ToList();
            return View(articles);
        }

        [HttpGet]
        public IActionResult Read([FromRoute]int id)
        {
            var article = _dbContext.Articles.FirstOrDefault(a => a.Id == id);

            if (article is null)
            {
                return RedirectToAction("Index");
            }

            return View(article);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}