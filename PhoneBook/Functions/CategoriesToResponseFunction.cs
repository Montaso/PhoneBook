using System;
using System.Collections.Generic;
using System.Linq;
using PhoneBook.src.Dtos;
using PhoneBook.src.Models;

namespace PhoneBook.src.Functions
{
    // function to refactor category to a json response
    public class CategoriesToResponseFunction
    {
        public static GetCategoriesResponse Apply(Category[] entities)
        {
            return new GetCategoriesResponse
            {
                Categories = entities.Select(category => new CategoryDto
                {
                    Name = category.Name
                }).ToList()
            };
        }
    }
}
