using System;
using System.Collections.Generic;
using PhoneBook.src.Models;

namespace PhoneBook.src.Dtos
{
    public class GetSimpleContactsResponse
    {
        public List<SimpleContactDto> Contacts { get; init; } = new();
    }

    public record SimpleContactDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Subcategory { get; set; }
    }
}
