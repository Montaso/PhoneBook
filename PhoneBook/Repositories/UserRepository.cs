using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PhoneBook.src.Models;

namespace PhoneBook.src.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Created<User>> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return TypedResults.Created($"/api/auth/{user.Login}", user);
        }

        public async Task<Results<Ok<User>, NotFound>> GetUserAsync(string login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Login == login);
            return user is not null ? TypedResults.Ok(user) : TypedResults.NotFound();
        }
    }
}