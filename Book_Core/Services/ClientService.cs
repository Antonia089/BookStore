using Book_Core.Interfaces;
using BookStore_Data;
using BookStore_Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Core.Services
{
    public class ClientService : IClientService
    {
        protected readonly BookStoreDataDbContext context;

        public ClientService(BookStoreDataDbContext context)
        {
            this.context = context;
        }

        public async Task AddClientAsync(Client client)
        {
            context.Clients.Add(client);
            await context.SaveChangesAsync();
        }

        public async Task DeleteClientAsync(int id)
        {
            var client = await context.Clients.
                   Include(p => p.Purchases)
                   .FirstOrDefaultAsync(p => p.Id == id);
            if (client != null)
            {
                context.Purchases.RemoveRange(client.Purchases);
                context.Clients.Remove(client);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Client>> GetAllClientsAsync()
        {
            return await context.Clients.
             Include(p => p.Purchases).
             ThenInclude(dp => dp.Book).
             ToListAsync();
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            return await context.Clients.Include(x=>x.Purchases).FirstOrDefaultAsync(c=>c.Id==id);
        }

        public async Task<Client> GetClientWithBookAsync(int id)
        {
            return await context.Clients
               .Include(p => p.Purchases)
               .ThenInclude(dp => dp.Book)
               .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateClientAsync(Client client, List<int> boookIds)
        {
            //client.Purchases.Clear();
            //foreach (var id in boookIds.Distinct())
            //{

            //    client.Purchases.Add(new Purchases
            //    {
            //        BookId = id,
            //        ClientId = client.Id,
            //        PurchaseDate = DateTime.Now
            //    });

            //}
            //await context.SaveChangesAsync();
            var selectedBookIds=boookIds.Distinct().ToList();
            var purchasesToRemove = client.Purchases.Where(p => !selectedBookIds.Contains(p.BookId)).ToList();
            foreach (var purchase in purchasesToRemove)
            {

                client.Purchases.Remove(purchase);
            }
            foreach (var bookId in selectedBookIds)
            {
                if (!client.Purchases.Any(p => p.BookId == bookId))
                {
                    client.Purchases.Add(new Purchases
                    {
                        BookId = bookId,
                        ClientId = client.Id,
                        PurchaseDate = DateTime.Now
                    });
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
