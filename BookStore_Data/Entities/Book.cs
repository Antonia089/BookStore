using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_Data.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(100)]
        public string Genres { get; set; }
        [Required]
        [Range(1900,2026)]
        public int YearOfPublication { get; set; }
        [Required]
        [Range(1,1000)]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }
        [Required]
        [Range(2,1000)]
        public int NumberOfPages { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }

    }
}
