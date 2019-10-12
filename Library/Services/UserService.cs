using System.Threading.Tasks;
using Library.Helpers;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class UserService : IUserService
    {
        private readonly LibraryContext _context;

        public UserService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync(string email, string password)
        {
            var hashedPassword = PasswordManager.HashPassword(password);
            return await _context.Users.FirstOrDefaultAsync(user => user.Email == email && user.Password == hashedPassword);
        }
    }
}
