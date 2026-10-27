using Book_Core.Interfaces;
using BookStore_ASP.ViewModel.Client;
using BookStore_Data;
using BookStore_Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookStore_ASP.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService clientService;
        private readonly IBookService bookService;

        public ClientController(IClientService patientService, IBookService doctorService)
        {
            this.clientService = patientService;
            this.bookService = doctorService;
        }

        public async Task<IActionResult> Index()
        {
            var clients = await clientService.GetAllClientsAsync();
            var model = new List<IndexClientViewModel>();
            foreach (var c in clients)
            {
                model.Add(new IndexClientViewModel
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PurchasedBookNames = string.Join(", ", c.Purchases.Select(dp => dp.Book.Title))
                });
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var client = await clientService.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            var model = new DetailsClientViewModel
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                BookNames = string.Join(", ", client.Purchases?
      .Where(dp => dp.Book != null)
      .Select(dp => dp.Book.Title) ?? new List<string>())
            };
            return View(model);
        }

        private async Task LoadBooks(List<int> selectBooks = null)
        {
            var b=await bookService.GetAllBooksAsync();
         
            ViewBag.Books = new MultiSelectList(
                b,
                "Id",
                "Title",
                selectBooks);
        }

        public async Task<IActionResult> Create()
        {
            await LoadBooks();
            var model = new CreateClientViewModel
            {
                PurcahsedOn = DateTime.Today
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateClientViewModel model)
        {
            var errors = ModelState.Where(kvp => kvp.Value.Errors.Any())
                           .Select(kvp => new { kvp.Key, Errors = kvp.Value.Errors.Select(e => e.ErrorMessage) });
            System.Diagnostics.Debug.WriteLine("ModelState errors: " + System.Text.Json.JsonSerializer.Serialize(errors));
            System.Diagnostics.Debug.WriteLine("Incoming BookIds: " + (model.BookIds == null ? "null" : string.Join(",", model.BookIds)));

            if (!ModelState.IsValid)
            {
                await LoadBooks(model.BookIds);
                return View(model);
            }

            var client = new BookStore_Data.Entities.Client
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            // create purchases for selected books
            foreach (var bookId in model.BookIds)
            {
                client.Purchases.Add(new BookStore_Data.Entities.Purchases
                {
                    BookId = bookId,
                    PurchaseDate = model.PurcahsedOn
                });
            }

           clientService.AddClientAsync(client);

            return RedirectToAction(nameof(Index));
        }

        // ... keep existing Edit/Delete/Index/Details methods unchanged ...
    
        public async Task<IActionResult> Edit(int id)
        {
            var client = await clientService.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            var model = new UpdateClientViewModel
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                BookIds = client.Purchases.Select(dp => dp.BookId).ToList()
            };
            await LoadBooks(model.BookIds);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, UpdateClientViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var client = await clientService.GetClientByIdAsync(id);
                if (client == null)
                { return NotFound(); }
                client.FirstName = model.FirstName;
                client.LastName = model.LastName;
                client.Email = model.Email;
                client.PhoneNumber = model.PhoneNumber;
                client.Purchases.Clear();
                foreach (var bookid in model.BookIds.Distinct())
                {
                    client.Purchases.Add(new Purchases
                    {
                        BookId = bookid,
                        ClientId = client.Id,
                        PurchaseDate = DateTime.Now  // Use the value from the view model
                    }); 
                   

                }
               await clientService.UpdateClientAsync(client,model.BookIds);
                return RedirectToAction(nameof(Index));
            }
            await LoadBooks(model.BookIds);
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var c = await clientService.GetClientByIdAsync(id);
            if (c == null)
            {
                return NotFound();
            }
            var model = new DeleteClientViewModel
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName
            };
            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await clientService.DeleteClientAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
