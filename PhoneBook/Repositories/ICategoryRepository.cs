using Microsoft.AspNetCore.Http.HttpResults;
using PhoneBook.src.Models;

namespace PhoneBook.Repositories
{
    public interface ICategoryRepository{

        Task<Category[]> GetAllCategoriesAsync();
        Task<Results<Ok<Category>, NotFound>> GetCategoryAsync(string name);

        Task<Results<Ok<Subcategory[]>, NotFound>> GetCategorySubcategoriesAsync(string name);
        Task<Created<Category>>AddCategoryAsync(Category category);
        Task<Results<NoContent, NotFound>>DeleteCategoryAsync(string name);

        Task<Subcategory[]> GetAllSubcategoriesAsync();
        Task<Results<Ok<Subcategory>, NotFound>> GetSubcategoryAsync(string name);
        Task<Created<Subcategory>>AddSubcategoryAsync(Subcategory subcategory);
        Task<Results<NoContent, NotFound>>DeleteSubcategoryAsync(string name);
    }
}