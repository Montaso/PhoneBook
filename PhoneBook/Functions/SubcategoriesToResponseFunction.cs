using System;
using System.Collections.Generic;
using System.Linq;
using PhoneBook.src.Dtos;
using PhoneBook.src.Models;

namespace PhoneBook.src.Functions
{
    public class SubcategoriesToResponseFunction
    {
        // function to handle subcategory get response
        public static GetSubcategoriesResponse Apply(Subcategory[] entities)
        {
            return new GetSubcategoriesResponse
            {
                Subcategories = entities.Select(subcategory => new SubcategoryDto
                {
                    Name = subcategory.Name.ToString(),
                }).ToList()
            };
        }
    }
}
