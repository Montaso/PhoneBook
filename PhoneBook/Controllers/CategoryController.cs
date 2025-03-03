using Microsoft.AspNetCore.Mvc;
using PhoneBook.src.Functions;
using PhoneBook.src.Services;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService) 
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var contacts = await _categoryService.GetAllCategoriesAsync();
        return Ok(CategoriesToResponseFunction.Apply(contacts));
    }
}