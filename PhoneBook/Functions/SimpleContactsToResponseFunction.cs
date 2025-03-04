using System;
using System.Collections.Generic;
using System.Linq;
using PhoneBook.src.Dtos;
using PhoneBook.src.Models;

namespace PhoneBook.src.Functions
{
    public class SimpleContactsToResponseFunction
    {
        public static GetSimpleContactsResponse Apply(Contact[] entities)
        {
            return new GetSimpleContactsResponse
            {
                Contacts = entities.Select(contact => new SimpleContactDto
                {
                    Id = contact.Id,
                    Name = contact.Name,
                    Surname = contact.Surname,
                    Email = contact.Email,
                    PhoneNumber = contact.PhoneNumber,
                    Subcategory = contact.Subcategory?.Name ?? "null"
                }).ToList()
            };
        }
    }
}
