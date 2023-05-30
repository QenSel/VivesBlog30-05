using Microsoft.EntityFrameworkCore;
using VivesBlog.Ui.Mvc.Models;

namespace VivesBlog.Ui.Mvc.Core
{
    public class VivesBlogDbContext: DbContext
    {
        public VivesBlogDbContext(DbContextOptions<VivesBlogDbContext> options): base(options)
        {
            
        }

        public DbSet<Article> Articles => Set<Article>();
        public DbSet<Person> People => Set<Person>();
        
        public void Seed()
        {
            var bavoPerson = new Person
            {
                FirstName = "Bavo",
                LastName = "Ketels",
                Email = "bavo.ketels@vives.be"
            };
            People.AddRange(new List<Person>
            {
                bavoPerson,
                new Person{FirstName = "Isabelle", LastName = "Vandoorne", Email = "isabelle.vandoorne@vives.be" },
                new Person
                {
                    FirstName = "Wim",
                    LastName = "Engelen",
                    Email = "wim.engelen@vives.be"
                },
                new Person{FirstName = "Ebe", LastName = "Deketelaere", Email = "ebe.deketelaere@vives.be" }
            });

            for (int i = 1; i <= 10; i++)
            {
                Articles.Add(new Article
                {
                    Id = i,
                    Title = $"Article title {i}",
                    Description = $"This is about article {i}",
                    Content = $"The full content of article {i}",
                    CreatedDate = DateTime.UtcNow.AddHours(-i),
                    Author = bavoPerson
                });
            }

            

            SaveChanges();
        }
    }
}
