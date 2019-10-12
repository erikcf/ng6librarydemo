using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Helpers;
using Library.Models;

namespace Library.Commands.Books
{
    public class CreateBookCommand : ICommand<Book, int>
    {
        public string Name { get; set; }
        public int Result { get; private set; }
        public async Task Execute(Book nullEntity, LibraryContext context)
        {
            var book = new Book
            {
                Name = Name
            };
            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
            Result = book.BookId;
        }

        public IEnumerable<string> Validate(Book entity, LibraryContext context)
        {
            var errors = new List<string>();
            ErrorHelper.AddError(errors, nameof(Name));
            return errors;
        }
    }
}
