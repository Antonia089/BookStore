using System.ComponentModel.DataAnnotations;

namespace BookStore_ASP.ViewModel.Author
{
    public class UpdateAuthorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The first name is required")]
        [MaxLength(100, ErrorMessage = "The max length is 100")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The last name is required")]
        [MaxLength(100, ErrorMessage = "The max length is 100")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The country name is required")]
        [MaxLength(100, ErrorMessage = "The max length is 100")]
        public string Country { get; set; }
    }
}
