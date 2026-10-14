using System.ComponentModel.DataAnnotations;

namespace BookStore_ASP.ViewModel.Client
{
    public class DetailsClientViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string BookNames { get; set; }
    }
}
