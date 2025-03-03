using Microsoft.AspNetCore.Http.HttpResults;
using PhoneBook.src.Models;

namespace PhoneBook.Repositories
{
    public interface IContactRepository{
        Task<Contact[]> GetAllContactsAsync();
        Task<Results<Ok<Contact>, NotFound>> GetContactAsync(string email);

        Task<Results<Ok<Contact>, NotFound>> GetContactByIdAsync(Guid id);

        Task<Created<Contact>>AddContactAsync(Contact contact);
        Task<Results<NoContent, NotFound>>UpdateContactAsync(Contact contact);
        Task<Results<NoContent, NotFound>>DeleteContactAsync(Guid id);
    }
}