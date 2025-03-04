using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Repositories;
using PhoneBook.src.Models;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Category[]> GetAllCategoriesAsync() 
    {
        return await _context.Categories.ToArrayAsync();
    }

    public async Task<Results<Ok<Category>, NotFound>> GetCategoryAsync(string name)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
        return category is not null ? TypedResults.Ok(category) : TypedResults.NotFound();
    }

    public async Task<Results<Ok<Subcategory[]>, NotFound>> GetCategorySubcategoriesAsync(string name)
    {
        var subcategories = await _context.Subcategories
            .Where(s => s.Category.Name == name)
            .ToArrayAsync();

        return TypedResults.Ok(subcategories);

    }

    public async Task<Created<Category>> AddCategoryAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return TypedResults.Created($"/api/contacts/{category.Name}", category);
    }

    public async Task<Results<NoContent, NotFound>> DeleteCategoryAsync(string name)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
        if (category is null) return TypedResults.NotFound();

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    public async Task<Subcategory[]> GetAllSubcategoriesAsync() 
    {
        return await _context.Subcategories.ToArrayAsync();
    }

    public async Task<Results<Ok<Subcategory>, NotFound>> GetSubcategoryAsync(string name)
    {
        var subcategory = await _context.Subcategories
        .Include(c => c.Contacts)
        .Include(d => d.Category)
        .FirstOrDefaultAsync(c => c.Name == name);
        return subcategory is not null ? TypedResults.Ok(subcategory) : TypedResults.NotFound();
    }

    public async Task<Created<Subcategory>> AddSubcategoryAsync(Subcategory subcategory)
    {
        if(subcategory.Category is null) {
            var otherCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Name == "Inny");

            if (otherCategory is null) throw new ArgumentException("No other category to assign subcategory to");
            
            subcategory.Category = otherCategory;
        }

        _context.Subcategories.Add(subcategory);
        await _context.SaveChangesAsync();
        return TypedResults.Created($"/api/contacts/{subcategory.Name}", subcategory);
    }

    public async Task<Results<NoContent, NotFound>> DeleteSubcategoryAsync(string name)
    {
        var subcategory = await _context.Subcategories.FirstOrDefaultAsync(c => c.Name.ToString() == name);
        if (subcategory is null) return TypedResults.NotFound();

        _context.Subcategories.Remove(subcategory);
        await _context.SaveChangesAsync();
        return TypedResults.NoContent();
    }

}
