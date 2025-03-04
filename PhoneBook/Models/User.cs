using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PhoneBook.src.Models
{
    [PrimaryKey(nameof(Login))]
    [Table("users")]
    public class User
    {
        [Column("login")]
        public string Login { get; set; }

        [Column("haslo")]
        public string Password { get; set; }

    }
}