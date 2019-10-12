using System.Threading.Tasks;
using Library.Dto;
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
            => await _context.Users.FirstOrDefaultAsync(user => user.Email == email && user.Password == password);
    }
}
