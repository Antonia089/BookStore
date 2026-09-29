using System.ComponentModel.DataAnnotations;

namespace BookStore_ASP.ViewModel.Book
{
    public class DeleteBookViewModel
    {
        
        public int Id { get; set; }
        [Required(ErrorMessage = "The title is required")]
        [MaxLength(100, ErrorMessage = "The max length is 100")]
        public string Title { get; set; }
    }
}
