using Library.Commands.Books;

namespace Library.RequestModels
{
    public class BookRequestModel
    {
        public string Name { get; set; }

        public CreateBookCommand ToCommand()
        {
            return new CreateBookCommand
            {
                Name = Name
            };
        }
    }
}
