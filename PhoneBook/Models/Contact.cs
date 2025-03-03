using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PhoneBook.src.Models
{
    [PrimaryKey(nameof(Id))]
    [Table("contacts")]
    public class Contact
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("imie")]
        public string Name { get; set; }

        [Column("nazwisko")]
        public string Surname { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("haslo")]
        public string Password { get; set; }

        [Column("telefon")]
        public string PhoneNumber { get; set; }

        [Column("data_urodzenia")]
        public DateOnly BirthDate { get; set; }

        [JsonIgnore]
        [ForeignKey("podkategoria")]
        public Subcategory Subcategory { get; set; }

        public void Update(Contact entity) {
            if (entity == null) return;

            this.Name = entity.Name;
            this.Surname = entity.Surname;
            this.Email = entity.Email;
            this.Password = entity.Password;
            this.PhoneNumber = entity.PhoneNumber;
            this.BirthDate = entity.BirthDate;
            this.Subcategory = entity.Subcategory;
        }
    }
}