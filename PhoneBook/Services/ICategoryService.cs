using Microsoft.AspNetCore.Http.HttpResults;
using PhoneBook.src.Models;

namespace PhoneBook.src.Services
{
    public interface ICategoryService
    {
        Task<Category[]> GetAllCategoriesAsync();

        Task<Results<Ok<Category>, NotFound>> GetCategoryAsync(string name);

        Task<Results<Ok<Subcategory[]>, NotFound>> GetCategorySubcategoriesAsync(string name);

        Task<Results<Created<Category>, NoContent>> AddCategoryAsync(Category category);

        Task<Results<Created<Subcategory>, NoContent>> AddSubcategoryAsync(Subcategory subcategory);
    }
}