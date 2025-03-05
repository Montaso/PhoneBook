using Microsoft.AspNetCore.Http.HttpResults;
using PhoneBook.Repositories;
using PhoneBook.src.Services;
using PhoneBook.src.Models;
using PhoneBook.src.PasswordHasher;

public class ContactService : IContactService
{
    private IContactRepository _contactRepository;
    private ICategoryRepository _categoryRepository;

    // inject repos into service
    public ContactService(IContactRepository contactRepository, ICategoryRepository categoryRepository) {
        this._contactRepository = contactRepository;
        this._categoryRepository = categoryRepository;
    }

    public async Task<Created<Contact>> AddContactAsync(Contact contact)
    {
        // check if contact subcategory exists
        var result = await _categoryRepository.GetSubcategoryAsync(contact.Subcategory.Name);
        if (result.Result is NotFound) {
            // add if it does not
            await _categoryRepository.AddSubcategoryAsync(contact.Subcategory);
        }

        // hash contact password
        contact.Password = PasswordHasher.HashPassword(contact.Password);

        return await _contactRepository.AddContactAsync(contact);
    }

    public async Task<Results<NoContent, NotFound>> DeleteContactAsync(string id)
    {
        return await _contactRepository.DeleteContactAsync(Guid.Parse(id));
    }

    public async Task<Contact[]> GetAllContactsAsync()
    {
        return await _contactRepository.GetAllContactsAsync();
    }

    public async Task<Results<Ok<Contact>, NotFound>> GetContactAsync(string email)
    {
        return await _contactRepository.GetContactAsync(email);
    }

    public async Task<Results<Ok<Contact>, NotFound>> GetContactByIdAsync(string id)
    {
        return await _contactRepository.GetContactByIdAsync(Guid.Parse(id));
    }

    public async Task<Results<NoContent, NotFound>> UpdateContactAsync(Contact contact)
    {
        // check if contacts subcategory exists
        var result = await _categoryRepository.GetSubcategoryAsync(contact.Subcategory.Name);
        if (result.Result is NotFound) {
            // add subcategory if it does not exist
            await _categoryRepository.AddSubcategoryAsync(contact.Subcategory);
        }
        // hash contact password
        contact.Password = PasswordHasher.HashPassword(contact.Password);

        return await _contactRepository.UpdateContactAsync(contact);
    }
}