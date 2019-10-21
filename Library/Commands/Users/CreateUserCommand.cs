using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Domain.Models;
using Library.Helpers;

namespace Library.Commands.Users
{
    public class CreateUserCommand : ICommand<User, int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Result { get; private set; }
        public async Task Execute(User nullEntity, LibraryContext context)
        {
            var user = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = PasswordManager.HashPassword(Password)
            };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            Result = user.UserId;
        }

        public IList<string> Validate(User nullEntity, LibraryContext context)
        {
            var errors = new List<string>();
            errors.AddError(nameof(FirstName));
            errors.AddError(nameof(LastName));
            errors.AddError(nameof(Email));
            errors.AddError(nameof(Password));
            return errors;
        }
    }
}
