using Microsoft.AspNetCore.Http.HttpResults;
using PhoneBook.src.Models;
using PhoneBook.src.Repositories;

namespace PhoneBook.src.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Results<Ok<User>, NotFound>> GetUserAsync(string login)
        {
            return await _userRepository.GetUserAsync(login);
        }

        public async Task<Results<Created<User>, NoContent>> AddUserAsync(User user)
        {
            // check if user exists
            var userResult = await _userRepository.GetUserAsync(user.Login);
            if (userResult.Result is NotFound)
            {
                // if does not exist hash password and use repository
                user.Password = PasswordHasher.PasswordHasher.HashPassword(user.Password);
                return await _userRepository.AddUserAsync(user);
            }
            return TypedResults.NoContent();
        }

        public async Task<bool> VerifyUser(User user)
        {
            var userResult = await _userRepository.GetUserAsync(user.Login); 

            if(userResult.Result is Ok<User> okResult)
            {
                // check if password matches the one retrieved from database 
                if(PasswordHasher.PasswordHasher.verifyPassword(user.Password, okResult.Value.Password))
                {
                    return true;
                } 
            }
            return false;
            
        }
    }
}