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
    public class PurchaseConfiguration : IEntityTypeConfiguration<Purchases>
    {
        public void Configure(EntityTypeBuilder<Purchases> builder)
        {
           builder.HasData(
                new Purchases
                {
                    ClientId = 1,
                    BookId = 1,
                    PurchaseDate =DateTime.Now,
                },
                new Purchases
                {
                    ClientId = 2,
                    BookId = 2,
                    PurchaseDate = DateTime.Now
                }
            );
        }
    }
}
