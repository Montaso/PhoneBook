using System;
using System.Collections.Generic;
using PhoneBook.src.Models;

namespace PhoneBook.src.Dtos
{
    public class GetCategoriesResponse
    {
        public List<CategoryDto> Categories { get; init; } = new();
    }

    public record CategoryDto
    {
        public string Name { get; set; }
    }
}
