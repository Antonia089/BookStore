using System.ComponentModel.DataAnnotations;

namespace BookStore_ASP.ViewModel.Author
{
    public class DetailsAuthorViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        [Display(Name = "Създадена на")]
        public DateTime CreatedOn { get; set; }
    }
}
