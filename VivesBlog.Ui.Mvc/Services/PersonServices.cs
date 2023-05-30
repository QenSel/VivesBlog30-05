using Microsoft.EntityFrameworkCore;
using VivesBlog.Ui.Mvc.Core;
using VivesBlog.Ui.Mvc.Models;

namespace VivesBlog.Ui.Mvc.Services
{
    public class PersonServices
    {
        private readonly VivesBlogDbContext _DbContext;

        public PersonServices(VivesBlogDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public IList<Person> Find()
        {
            return _DbContext.People.ToList();
        }

        public Person Get(int id)
        {
            return _DbContext.People.Find(id);
        }

        public Person? Create(Person person)
        {
            _DbContext.Add(person);
            _DbContext.SaveChanges();

            return person;
        }

        //Update
        public Person? Update(int id, Person person)
        {
            var dbPerson = _DbContext.People.Find(id);
            if (dbPerson is null)
            {
                return null;
            }

            dbPerson.Id = person.Id;
            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;
            dbPerson.Email = person.Email;
            dbPerson.Articles = person.Articles;
            

            _DbContext.SaveChanges();

            return dbPerson;
        }

        //Delete
        public void Delete(int id)
        {
            var person = new Person
            {
                Id = id,
                FirstName = string.Empty,
                LastName = string.Empty,
                Email = string.Empty,
                Articles = new List<Article>()
                    
                
            };
            _DbContext.People.Attach(person);
            _DbContext.People.Remove(person);

            _DbContext.SaveChanges();
        }
    }
}
