using PhoneBook.src.Dtos;
using PhoneBook.src.Models;

namespace PhoneBook.src.Functions
{
    public class RequestToContactFunction
    {
        // function to refactor a contact request contact object
        public static Contact Apply(Guid id, PutContactRequest request)
        {
            return new Contact
            {
                Id = id,
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber,
                BirthDate = request.BirthDate,
                Subcategory = new Subcategory
                {
                    Name = request.Subcategory,
                }

            };
        }
    }
}