using PhoneBook.src.Dtos;
using PhoneBook.src.Models;

namespace PhoneBook.src.Functions
{
public class ContactToResponseFunction
{
    public static GetContactResponse Apply(Contact entity)
    {
        return new GetContactResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Surname = entity.Surname,
            Email = entity.Email,
            Password = entity.Password,
            PhoneNumber = entity.PhoneNumber,
            BirthDate = entity.BirthDate,
            Subcategory = entity.Subcategory.Name
        };
    }
}
}
