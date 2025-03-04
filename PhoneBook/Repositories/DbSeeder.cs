using Microsoft.AspNetCore.Identity;
using PhoneBook.Repositories;
using PhoneBook.src.Models;
using PhoneBook.src.PasswordHasher;

public static class DbSeeder
{
    public static async Task SeedAsync(IContactRepository contactRepository, ICategoryRepository categoryRepository)
    {
        var categories = await categoryRepository.GetAllCategoriesAsync();
        if (!categories.Any())
        {
            var newCategories = new List<Category>()
            {
                new Category { Name = "Prywatny", Subcategories = new List<Subcategory>() },
                new Category { Name = "Służbowy", Subcategories = new List<Subcategory>() },
                new Category { Name = "Inny", Subcategories = new List<Subcategory>() }
            };

            foreach (var category in newCategories)
            {
                await categoryRepository.AddCategoryAsync(category);
            }
        }

        categories = await categoryRepository.GetAllCategoriesAsync();

        var subcategories = await categoryRepository.GetAllSubcategoriesAsync();
        if (!subcategories.Any())
        {
            var newSubcategories = new List<Subcategory>()
            {
                new Subcategory { Name = "szef", Category = categories.ElementAt(1), Contacts = new List<Contact>()},
                new Subcategory { Name = "klient",  Category = categories.ElementAt(1), Contacts = new List<Contact>()},
                new Subcategory { Name = "prywatny",  Category = categories.First(), Contacts = new List<Contact>()},
                new Subcategory { Name = "dostawca pizzy",  Category = categories.Last(), Contacts = new List<Contact>()}
            };

            foreach (var subcategory in newSubcategories)
            {
                await categoryRepository.AddSubcategoryAsync(subcategory);
            }
        }

        subcategories = await categoryRepository.GetAllSubcategoriesAsync();

        var contacts = await contactRepository.GetAllContactsAsync();
        if (!contacts.Any())
        {
            var sampleContacts = new List<Contact>
            {
                new Contact { Id = Guid.NewGuid(), Name = "Jan", Surname = "Kowalski", Email = "jan@kowalski.com", PhoneNumber = "111111111", BirthDate = new DateOnly(1999, 12, 11), Password = PasswordHasher.HashPassword("qwe123"), Subcategory = subcategories.First()},
                new Contact { Id = Guid.NewGuid(), Name = "Janusz", Surname = "Nowak", Email = "janusz@nowak.com", PhoneNumber = "222222222", BirthDate = new DateOnly(1988, 1, 13), Password = PasswordHasher.HashPassword("qwe1234"), Subcategory = subcategories.ElementAt(1)},
                new Contact { Id = Guid.NewGuid(), Name = "Mariusz", Surname = "Siemaszko", Email = "mariusz@siemaszko.com", PhoneNumber = "333333333", BirthDate = new DateOnly(1992, 3, 2), Password = PasswordHasher.HashPassword("qwe12322"), Subcategory = subcategories.ElementAt(1)},
                new Contact { Id = Guid.NewGuid(), Name = "Paweł", Surname = "Bóbr", Email = "pawel@bobr.com", PhoneNumber = "444444444", BirthDate = new DateOnly(2000, 4, 28), Password = PasswordHasher.HashPassword("qwe123p4ssw0rd"), Subcategory = subcategories.ElementAt(2)},
                new Contact { Id = Guid.NewGuid(), Name = "Michał", Surname = "Ptak", Email = "michal@ptak.com", PhoneNumber = "555555555", BirthDate = new DateOnly(2001, 7, 7), Password = PasswordHasher.HashPassword("h4sl0_h4sl0"), Subcategory = subcategories.Last()},
            };

            foreach (var contact in sampleContacts)
            {
                await contactRepository.AddContactAsync(contact);
            }
        }
    }
}
