using Microsoft.AspNetCore.Http.HttpResults;
using PhoneBook.Repositories;
using PhoneBook.src.Models;

namespace PhoneBook.src.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;

        // inject repos into the service
        public CategoryService(ICategoryRepository categoryRepository) {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category[]> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task<Results<Ok<Category>, NotFound>> GetCategoryAsync(string name)
        {
            return await _categoryRepository.GetCategoryAsync(name);
        }

        public async Task<Results<Ok<Subcategory[]>, NotFound>> GetCategorySubcategoriesAsync(string name)
        {
            return await _categoryRepository.GetCategorySubcategoriesAsync(name);
        }

        public async Task<Results<Created<Category>, NoContent>> AddCategoryAsync(Category category)
        {
            // check if category already exists
            var existingCategory = await _categoryRepository.GetCategoryAsync(category.Name);
            if (existingCategory.Result is Ok<Category> okResult)
            {
                // if category exists return nocontent
                return TypedResults.NoContent();
            }

            return await _categoryRepository.AddCategoryAsync(category);
        }

        public async Task<Results<Created<Subcategory>, NoContent>> AddSubcategoryAsync(Subcategory subcategory)
        {
            // check if subcategory exists
            var existingSubcategory = await _categoryRepository.GetSubcategoryAsync(subcategory.Name);
            if (existingSubcategory.Result is Ok<Subcategory> okResult)
            {
                // if subcategory exists return nocontent
                return TypedResults.NoContent();
            }

            return await _categoryRepository.AddSubcategoryAsync(subcategory);
        }

    }
}