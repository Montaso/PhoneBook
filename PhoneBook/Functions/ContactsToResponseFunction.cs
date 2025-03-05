using System;
using System.Collections.Generic;
using System.Linq;
using PhoneBook.src.Dtos;
using PhoneBook.src.Models;

namespace PhoneBook.src.Functions
{
    // function to refactor contacts to a json response
    public class ContactsToResponseFunction
    {
        public static GetContactsResponse Apply(Contact[] entities)
        {
            return new GetContactsResponse
            {
                Contacts = entities.Select(contact => new ContactDto
                {
                    Id = contact.Id,
                    Name = contact.Name,
                    Surname = contact.Surname,
                    Email = contact.Email,
                    Password = contact.Password,
                    PhoneNumber = contact.PhoneNumber,
                    BirthDate = contact.BirthDate,
                    Subcategory = contact.Subcategory?.Name ?? "null"
                }).ToList()
            };
        }
    }
}
