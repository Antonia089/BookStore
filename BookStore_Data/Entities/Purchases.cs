using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_Data.Entities
{
    public class Purchases
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int BookId { get; set; }
        public  Book Book { get; set; }
        [Column(TypeName ="datetime")]
        public DateTime PurchaseDate { get; set; }
    }
}
