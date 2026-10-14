using System.ComponentModel.DataAnnotations;

namespace BookStore_ASP.ViewModel.Client
{
    public class UpdateClientViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The first name is required")]
        [MaxLength(100, ErrorMessage = "The max length is 100")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The last name is required")]
        [MaxLength(100, ErrorMessage = "The max length is 100")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The email is required")]
        [MaxLength(100, ErrorMessage = "The max length is 100")]
        [EmailAddress(ErrorMessage = "Email has to be")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The phone number is required")]
        public string PhoneNumber { get; set; }
        public List<int> BookIds { get; set; } = new List<int>();
    }
}
