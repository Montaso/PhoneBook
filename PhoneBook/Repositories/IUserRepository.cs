using Microsoft.AspNetCore.Http.HttpResults;
using PhoneBook.src.Models;

namespace PhoneBook.src.Repositories
{
    public interface IUserRepository
    {
           Task<Results<Ok<User>, NotFound>> GetUserAsync(string login);

           Task<Created<User>> AddUserAsync(User user);
    }
}