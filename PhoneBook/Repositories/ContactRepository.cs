using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Repositories;
using PhoneBook.src.Models;

public class ContactRepository : IContactRepository
{
    private readonly ApplicationDbContext _context;

    public ContactRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Contact[]> GetAllContactsAsync()
    {
        return await _context.Contacts
        .Include(c => c.Subcategory)
        .ToArrayAsync();
    }

    public async Task<Results<Ok<Contact>, NotFound>> GetContactAsync(string email)
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Email == email);
        return contact is not null ? TypedResults.Ok(contact) : TypedResults.NotFound();
    }

    public async Task<Results<Ok<Contact>, NotFound>> GetContactByIdAsync(Guid id)
    {
        var contact = await _context.Contacts
        .Include(c => c.Subcategory)
        .FirstOrDefaultAsync(c => c.Id == id);

        if (contact is not null) return TypedResults.Ok(contact);
        else return TypedResults.NotFound();
    }

    public async Task<Created<Contact>> AddContactAsync(Contact contact)
    {
        // check if subcategory is filled
        if(contact.Subcategory != null)
        {
            var existingSubcategory = await _context.Subcategories
            .FirstOrDefaultAsync(s => s.Name == contact.Subcategory.Name);

            if (existingSubcategory != null)
            {
                contact.Subcategory = existingSubcategory;
            }
        }
        
        // check if contact is already attached 
        var existingContact = await _context.Contacts.FindAsync(contact.Id);
        if (existingContact != null)
        {
            _context.Entry(existingContact).State = EntityState.Detached;
        }

        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();
        return TypedResults.Created($"/api/contacts/{contact.Email}", contact);
    }

    public async Task<Results<NoContent, NotFound>> UpdateContactAsync(Contact contact)
    {
        if(contact.Subcategory != null)
        {
            var existingSubcategory = await _context.Subcategories
            .FirstOrDefaultAsync(s => s.Name == contact.Subcategory.Name);

            if (existingSubcategory != null)
            {
                contact.Subcategory = existingSubcategory;
            }
        }

        var existingContact = await _context.Contacts.FindAsync(contact.Id);
        if (existingContact != null)
        {
            _context.Entry(existingContact).State = EntityState.Detached;
        }
        _context.Contacts.Update(contact);
        await _context.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    public async Task<Results<NoContent, NotFound>> DeleteContactAsync(Guid id)
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);
        if (contact is null) return TypedResults.NotFound();

        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync();
        return TypedResults.NoContent();
    }

}
