using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore_ASP.ViewModel.Book
{
    public class IndexBookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genres { get; set; }
        public string AuthorName { get; set; }
    }
}
