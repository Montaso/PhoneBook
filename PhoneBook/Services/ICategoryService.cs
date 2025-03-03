using Microsoft.AspNetCore.Http.HttpResults;
using PhoneBook.src.Models;

namespace PhoneBook.src.Services
{
    public interface ICategoryService
    {
        Task<Category[]> GetAllCategoriesAsync();
    }
}