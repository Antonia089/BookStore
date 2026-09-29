using System.ComponentModel.DataAnnotations;

namespace BookStore_ASP.ViewModel.Book
{
    public class UpdateBookViewModel
    {
       
        public int Id { get; set; }
        [Required(ErrorMessage = "The title is required")]
        [MaxLength(100, ErrorMessage = "The max length is 100")]
        public string Title { get; set; }
        [Required(ErrorMessage = "The genre is required")]
        [MaxLength(100, ErrorMessage = "The max length is 100")]
        public string Genres { get; set; }
        [Required(ErrorMessage = "The year of publishing is required")]
        [Range(1900, 2026, ErrorMessage = "The year of publishing has to be in range 1900 to 2026")]
        public int YearOfPublication { get; set; }
        [Required(ErrorMessage = "The price is required")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "The number of pages is required")]
        [Range(2, 1000, ErrorMessage = "Tge number of pages has to be in the range from 2 to 1000 pages")]
        public int NumberOfPages { get; set; }
        [Required(ErrorMessage = "Choose author")]
        public int AuthorId { get; set; }
    }
}
