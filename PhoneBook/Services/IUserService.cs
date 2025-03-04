using Microsoft.AspNetCore.Http.HttpResults;
using PhoneBook.src.Models;

namespace PhoneBook.src.Services
{
    public interface IUserService
    {
        Task<Results<Ok<User>, NotFound>> GetUserAsync(string login);
        Task<Results<Created<User>, NoContent>> AddUserAsync(User user);
        Task<bool> VerifyUser(User user);
    }
}