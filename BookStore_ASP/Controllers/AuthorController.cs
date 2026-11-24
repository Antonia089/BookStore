using Book_Core.Interfaces;
using Book_Core.Services;
using BookStore_ASP.Models;
using BookStore_ASP.ViewModel.Author;
using BookStore_Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore_ASP.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService service;

        public AuthorController(IAuthorService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            var authours = await service.GetAllAuthorsAsync();

            var model = new List<IndexAuthorViewModel>();

            foreach (var a in authours)
            {
                model.Add(new IndexAuthorViewModel
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Country = a.Country
                });
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> GetFilteredAuthors(string? search, string? specialty, string? sortOrder)
        {
            var authors = await service.GetAllAuthorsAsync();

            // Filter by name
            if (!string.IsNullOrWhiteSpace(search))
            {
                authors = authors.Where(a => 
                    a.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    a.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            // Filter by country
            if (!string.IsNullOrWhiteSpace(specialty))
            {
                authors = authors.Where(a => 
                    a.Country.Contains(specialty, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            // Sort - ensure proper sorting
            if (sortOrder == "name_desc")
            {
                authors = authors.OrderByDescending(a => a.FirstName).ThenByDescending(a => a.LastName).ToList();
            }
            else if (sortOrder == "country_asc")
            {
                authors = authors.OrderBy(a => a.Country).ThenBy(a => a.FirstName).ToList();
            }
            else // name_asc is default
            {
                authors = authors.OrderBy(a => a.FirstName).ThenBy(a => a.LastName).ToList();
            }

            var model = authors.Select(a => new IndexAuthorViewModel
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Country = a.Country
            }).ToList();

            return Json(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            var author = await service.GetAuthorByIdAsync(id);

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

               service.AddAuthorAsync(author);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var a = await service.GetAuthorByIdAsync(id);

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
                var au = await service.GetAuthorByIdAsync(id);

                if (au == null)
                {
                    return NotFound();
                }

                au.FirstName = model.FirstName;
                au.LastName = model.LastName;
                au.Country = model.Country;

               await service.UpdateAuthorAsync(au);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var authors = await service.GetAuthorByIdAsync(id);

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
            try
            {
                await service.DeleteAuthorAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

