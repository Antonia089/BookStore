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
        private readonly BookStoreDataDbContext context;

        public BookController(BookStoreDataDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var b = await context.Books
                .Include(d => d.Author)
                .ToListAsync();

            var model = new List<IndexBookViewModel>();

            foreach (var a in b )
            {
                model.Add(new IndexBookViewModel
                {
                    Id = a.Id,
                   Title = a.Title,
                     Genres = a.Genres,
                     AuthorName = a.Author.FirstName + " " + a.Author.LastName
                });
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var b = await context.Books
                .Include(d => d.Author)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (b == null)
            {
                return NotFound();
            }

            var model = new DetailesBookViewModel
            {
                Title=b.Title,
                Genres=b.Genres,
                Price=b.Price,
                NumberOfPages=b.NumberOfPages,
                YearOfPublication=b.YearOfPublication,
                AuthorName=b.Author.FirstName + " " + b.Author.LastName
            };

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Authors = new SelectList(
         await context.Authors
             .Where(a => !a.IsDeleted)
             .Select(a => new
             {
                 Id = a.Id,
                 FullName = a.FirstName + " " + a.LastName
             })
             .ToListAsync(),
         "Id",
         "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var b = new Book
                {
                    Title = model.Title,
                    Genres = model.Genres,
                    Price = model.Price,
                    NumberOfPages = model.NumberOfPages,
                    YearOfPublication = model.YearOfPublication,
                    AuthorId = model.AuthorId
                };

                context.Books.Add(b);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Authors = new SelectList(
            await context.Authors
                .Where(a => !a.IsDeleted)
                .Select(a => new
                {
                    Id = a.Id,
                    FullName = a.FirstName + " " + a.LastName
                })
                .ToListAsync(),
            "Id",
            "FullName");


            return View(model);
        }

       
        public async Task<IActionResult> Edit(int id)
        {
            var book = await context.Books.FindAsync(id);

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
        public async Task<IActionResult> Edit(int id,  UpdateBookViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await LoadAuthors();

                return View(model);
            }

            var book = await context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            book.Title = model.Title;
            book.Genres = model.Genres;
            book.Price = model.Price;
            book.NumberOfPages = model.NumberOfPages;
            book.YearOfPublication = model.YearOfPublication;
            book.AuthorId = model.AuthorId;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadAuthors()
        {
            ViewBag.Authors = new SelectList(
                await context.Authors
                    .Where(a => !a.IsDeleted)
                    .Select(a => new
                    {
                        Id = a.Id,
                        FullName = a.FirstName + " " + a.LastName
                    })
                    .ToListAsync(),
                "Id",
                "FullName");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var b = await context.Books.FindAsync(id);

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
            var book = await context.Books.FindAsync(id);

            if (book != null)
            {
                context.Books.Remove(book);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
