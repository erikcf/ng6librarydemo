using Library.Commands.Users;

namespace Library.RequestModels
{
    public class UserRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public CreateUserCommand ToCommand()
        {
            return new CreateUserCommand
            {
                FirstName = FirstName,
                LastName = LastName,
                Password = Password,
                Email = Email
            };
        }
    }
}
