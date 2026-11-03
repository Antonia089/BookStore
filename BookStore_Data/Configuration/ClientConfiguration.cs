using BookStore_Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_Data.Configuration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasData
                 (
                new Client
                {
                    Id = 1,
                    FirstName = "Olivia",
                    LastName = "Smith",
                    Email = "OliviaS@gmail.com",
                    PhoneNumber = "1234567890"
                },
                new Client
                {
                    Id = 2,
                    FirstName = "Liam",
                    LastName = "Johnson",
                    Email = "Liam123@gmail.com",
                    PhoneNumber = "0987654321"
                }
                );
        }
    }
}
