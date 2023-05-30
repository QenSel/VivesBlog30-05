using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using VivesBlog.Ui.Mvc.Core;
using VivesBlog.Ui.Mvc.Models;
using VivesBlog.Ui.Mvc.Services;

namespace VivesBlog.Ui.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly PersonServices _PersonServices;
        private readonly ArticleServices _ArticleServices;

        public HomeController(PersonServices PersonServices,ArticleServices ArticleServices)
        {
            _PersonServices = PersonServices;
            _ArticleServices = ArticleServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var articles = _ArticleServices.Find();
            return View(articles);
        }

        [HttpGet]
        public IActionResult Read([FromRoute]int id)
        {
            var article = _ArticleServices.ReadAll(id);

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