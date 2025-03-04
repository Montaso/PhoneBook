using System;
using System.Collections.Generic;
using PhoneBook.src.Models;

namespace PhoneBook.src.Dtos
{
    public class GetSubcategoriesResponse
    {
        public List<SubcategoryDto> Subcategories { get; init; } = new();
    }

    public record SubcategoryDto
    {
        public string Name { get; set; }
    }
}
