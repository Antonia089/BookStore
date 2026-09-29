using BookStore_ASP.Models;
using BookStore_ASP.ViewModel.Author;
using BookStore_Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore_ASP.Controllers
{
    public class AuthorController : Controller
    {
        public readonly BookStoreDataDbContext context;

        public AuthorController(BookStoreDataDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var authours = await context.Authors
                .Where(h => !h.IsDeleted)
                .ToListAsync();

            var model = new List<IndexAuthorViewModel>();

            foreach (var a in authours)
            {
                model.Add(new IndexAuthorViewModel
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName
                });
            }

            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            var author = await context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            var model = new DetailsAuthorViewModel
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Country = author.Country,
                CreatedOn = author.CreatedOn
            };

            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var author = new BookStore_Data.Entities.Author
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Country = model.Country,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false
                };

                context.Authors.Add(author);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var a = await context.Authors.FindAsync(id);

            if (a == null)
            {
                return NotFound();
            }

            var model = new UpdateAuthorViewModel
            {
                Id = a.Id,
                FirstName =a.FirstName,
                LastName = a.LastName,
                Country = a.Country
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UpdateAuthorViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var au = await context.Authors.FindAsync(id);

                if (au == null)
                {
                    return NotFound();
                }

                au.FirstName = model.FirstName;
                au.LastName = model.LastName;
                au.Country = model.Country;

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var authors = await context.Authors.FindAsync(id);

            if (authors == null)
            {
                return NotFound();
            }

            var model = new DeleteAuthorViewModel
            {
                Id = authors.Id,
                FirstName = authors.FirstName,
                LastName = authors.LastName
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var au = await context.Authors.FindAsync(id);

            if (au != null)
            {
                au.IsDeleted = true;

                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
