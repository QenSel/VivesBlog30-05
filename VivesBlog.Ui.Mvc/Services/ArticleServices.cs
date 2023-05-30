using VivesBlog.Ui.Mvc.Core;
using VivesBlog.Ui.Mvc.Models;

namespace VivesBlog.Ui.Mvc.Services
{
    public class ArticleServices
    {
        private readonly VivesBlogDbContext _DbContext;

        public ArticleServices(VivesBlogDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public IList<Person> GetAll()
        {
            return _DbContext.People.ToList();
        }

        public IList<Article> Find()
        {
            return _DbContext.Articles.OrderBy
                (x => x.Author).ToList();
        }

        public Article ReadAll(int id)
        {
            return _DbContext.Articles.FirstOrDefault(x => x.Id == id);
        
        }


        public Article Get(int id)
        {
            return _DbContext.Articles.Find(id);
        }

        public Article? Create(Article article)
        {
            _DbContext.Add(article);
            _DbContext.SaveChanges();

            return article;
        }

        //Update
        public Article? Update(int id, Article article)
        {
            var dbArticle = _DbContext.Articles.Find(id);
            if (dbArticle is null)
            {
                return null;
            }

            dbArticle.AuthorId = article.AuthorId;

            dbArticle.Author = article.Author;

            _DbContext.SaveChanges();

            return dbArticle;
        }

        //Delete
        public void Delete(int id)
        {
            var article = new Article
            {
                Id = id,
                Title = string.Empty,
                Description = string.Empty,
                Content = string.Empty,
                CreatedDate = DateTime.Now,
               
            };

            _DbContext.Articles.Attach(article);
            _DbContext.Articles.Remove(article);

            _DbContext.SaveChanges();
        }

    }
}
