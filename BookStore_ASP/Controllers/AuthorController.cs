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
                    LastName = a.LastName
                });
            }

            return View(model);
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

               service.UpdateAuthorAsync(au);

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
           
                await service.DeleteAuthorAsync(id);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
