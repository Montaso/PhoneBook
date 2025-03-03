using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.src.Models
{
    [PrimaryKey(nameof(Name))]
    [Table("categories")]
    public class Category
    {
        [Column("nazwa", TypeName = "text")]
        public CategoryEnum Name { get; set; }
        public ICollection<Subcategory> Subcategories { get; set; }
    }
}