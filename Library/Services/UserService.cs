using System.Linq;
using System.Threading.Tasks;
using Library.Commands;
using Library.Domain.Models;
using Library.Dtos;
using Library.Helpers;
using Library.RequestModels;
using Library.Results;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class UserService : IUserService
    {
        private readonly LibraryContext _context;
        private readonly ICommandRunner _commandRunner;

        public UserService(LibraryContext context, ICommandRunner commandRunner)
        {
            _context = context;
            _commandRunner = commandRunner;
        }

        public async Task<UserDto> GetUserAsync(string email, string password)
        {
            var hashedPassword = PasswordManager.HashPassword(password);
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email && user.Password == hashedPassword);
            return user is null ? null : UserDto.FromUser(user);
        }

        public async Task<UserResult> CreateUserAsync(UserRequestModel userRequestModel)
        {
            var command = userRequestModel.ToCommand();
            var validationErrors = _commandRunner.Validate(command, null);
            if (validationErrors.Any())
            {
                return new UserResult { ValidationErrors = validationErrors };
            }
            var id = await _commandRunner.Execute(command, null);
            var userDto = await GetUserByIdAsync(id);
            return new UserResult { UserDto = userDto };
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(b => b.UserId == id);
            return user is null ? null : UserDto.FromUser(user);
        }
    }
}
