namespace BookStore_ASP.ViewModel.Book
{
    public class DetailesBookViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Genres { get; set; }

        public int YearOfPublication { get; set; }

        public decimal Price { get; set; }

        public int NumberOfPages { get; set; }
        public string AuthorName { get; set; }
    }
}
