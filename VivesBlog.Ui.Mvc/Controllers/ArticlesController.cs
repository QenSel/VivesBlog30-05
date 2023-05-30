using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VivesBlog.Ui.Mvc.Core;
using VivesBlog.Ui.Mvc.Models;
using VivesBlog.Ui.Mvc.Services;

namespace VivesBlog.Ui.Mvc.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ArticleServices _ArticleSercices;

        public ArticlesController(ArticleServices ArticleServices)
        {
          _ArticleSercices = ArticleServices;
        }

        public IActionResult Index()
        {
            var articles = _ArticleSercices.Find();
                
            return View(articles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return CreateEditView("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Article article)
        {
            if (!ModelState.IsValid)
            {
                return CreateEditView("Create", article);
            }

            article.CreatedDate = DateTime.UtcNow;

            _ArticleSercices.Create(article);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var article = _ArticleSercices.Get(id);

            if (article is null)
            {
                return RedirectToAction("Index");
            }

            return CreateEditView("Edit", article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, Article article)
        {
            if (!ModelState.IsValid)
            {
                return CreateEditView("Edit", article);
            }

            var dbArticle = _ArticleSercices.Update(id, article);

            if (dbArticle is null)
            {
                return RedirectToAction("Index");
            }

            dbArticle.Title = article.Title;
            dbArticle.AuthorId = article.AuthorId;
            dbArticle.Description = article.Description;
            dbArticle.Content = article.Content;

            

            return RedirectToAction("Index");
        }

        private IActionResult CreateEditView([AspMvcView] string viewName, Article? article = null)
        {
            var people = _ArticleSercices.GetAll();

            ViewBag.People = people;

            return View(viewName, article);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var article = _ArticleSercices.Get(id);

            if (article is null)
            {
                return RedirectToAction("Index");
            }

            return View(article);
        }

        [HttpPost("Articles/Delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var article = new Article
            {
                Id = id,
                Title = string.Empty,
                Description = string.Empty,
                Content = string.Empty,
            };
            _ArticleSercices.Delete(id);
           

            return RedirectToAction("Index");
        }
    }
}