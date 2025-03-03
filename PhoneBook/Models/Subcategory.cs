using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PhoneBook.src.Models
{
    [PrimaryKey(nameof(Name))]
    [Table("subcategories")]
    public class Subcategory
    {
        [Column("nazwa", TypeName = "text")]
        public string Name { get; set; }

        [JsonIgnore]
        [ForeignKey("kategoria")]
        public Category Category { get; set; }

        [JsonIgnore]
        public ICollection<Contact> Contacts { get; set; }
    }
}