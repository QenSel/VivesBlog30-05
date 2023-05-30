using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VivesBlog.Ui.Mvc.Models
{
    public class Person
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }

        [ForeignKey(nameof(Article.AuthorId))]
        public IList<Article> Articles { get; set; } = new List<Article>();
    }
}
