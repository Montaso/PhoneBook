using System;
using System.Collections.Generic;
using PhoneBook.src.Models;

namespace PhoneBook.Dtos
{
    public class GetContactsResponse
    {
        public List<ContactDto> Contacts { get; init; } = new();
    }

    public record ContactDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Subcategory { get; set; }
    }
}
