using Book_Core.Interfaces;
using BookStore_ASP.ViewModel.Book;
using BookStore_Data;
using BookStore_Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace BookStore_ASP.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService bookService;
        private readonly IAuthorService  authorService;

        public BookController(IBookService b, IAuthorService h)
        {
            this.bookService = b;
            this.authorService = h;
        }

        public async Task<IActionResult> Index()
        {
            var b = await bookService.GetAllBooksAsync();

            var model = new List<IndexBookViewModel>();

            foreach (var a in b )
            {
                model.Add(new IndexBookViewModel
                {
                    Id = a.Id,
                   Title = a.Title,
                     Genres = a.Genres,
                     AuthorName = a.Author.FullName
                });
            }

            return View(model);
        }

       
            public async Task<IActionResult> Details(int id)
        {
            var b = await bookService.GetBookDetailsAsync(id);

            if (b == null)
            {
                return NotFound();
            }

            var model = new DetailesBookViewModel
            {
                Title = b.Title,
                Genres = b.Genres,
                Price = b.Price,
                NumberOfPages = b.NumberOfPages,
                YearOfPublication = b.YearOfPublication,
                AuthorName = b.Author.FullName,
                Clients = b.Purchases.Select(p => p.Client.FirstName + " " + p.Client.LastName).ToList()
            };

            return View(model);

        }
        
        
          
            
            [HttpGet]
            public async Task<IActionResult> Create()
            {
                var authors = await authorService.GetAllAuthorsAsync();

                if (authors == null)
                {
                    throw new Exception("Authors list is NULL!");
                }

                var model = new CreateBookViewModel();

            ViewBag.Authors = new SelectList(
            authors,
             "Id",
             "FullName"
            );

            return View(model);
            }
        
        


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadAuthors();
                return View(model);
            }

            var book = new Book
            {
                Title = model.Title,
                Genres = model.Genres,
                Price = model.Price,
                NumberOfPages = model.NumberOfPages,
                YearOfPublication = model.YearOfPublication,
                AuthorId = model.AuthorId
            };

            await bookService.AddBookAsync(book);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id)
        {
            var book = await bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            var model = new UpdateBookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Genres = book.Genres,
                Price = book.Price,
                NumberOfPages = book.NumberOfPages,
                YearOfPublication = book.YearOfPublication,
                AuthorId = book.AuthorId
            };

           

                await LoadAuthors();

                return View(model);
        
        }

          [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(
     int id,
     UpdateBookViewModel model)
            {
                if (id != model.Id)
                    return NotFound();

                if (!ModelState.IsValid)
                {
                    await LoadAuthors();
                    return View(model);
                }

                var book = await bookService.GetBookByIdAsync(id);

                if (book == null)
                    return NotFound();

                book.Title = model.Title;
                book.Genres = model.Genres;
                book.Price = model.Price;
                book.NumberOfPages = model.NumberOfPages;
                book.YearOfPublication = model.YearOfPublication;
                book.AuthorId = model.AuthorId;

                await bookService.UpdateBookAsync(book);

                return RedirectToAction(nameof(Index));
            }
        

        private async Task LoadAuthors()
        {
            var a = await authorService.GetAllAuthorsAsync();
            ViewBag.Authors = new SelectList(
        a,
         "Id",
         "FullName");

      
        }

        public async Task<IActionResult> Delete(int id)
        {
            var b = await bookService.GetBookByIdAsync(id);

            if (b == null)
            {
                return NotFound();
            }

            var model = new DeleteBookViewModel
            {
                Id = b.Id,
                Title=b.Title
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           await bookService.DeleteBookAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
