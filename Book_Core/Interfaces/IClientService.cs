using BookStore_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Core.Interfaces
{
    public interface IClientService
    {
        Task<List<Client>> GetAllClientsAsync();
        Task<Client> GetClientByIdAsync(int id);
        Task<Client> GetClientWithBookAsync(int id);
        Task AddClientAsync(Client patient);
        Task UpdateClientAsync(Client client, List<int> boookIds);
        Task DeleteClientAsync(int id);
    }
}
