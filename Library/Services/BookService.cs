using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Commands;
using Library.Domain.Models;
using Library.Dtos;
using Library.RequestModels;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryContext _context;
        private readonly CommandRunner _commandRunner;

        public BookService(LibraryContext context, CommandRunner commandRunner)
        {
            _context = context;
            _commandRunner = commandRunner;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync(string name)
        {
            var allBooks = await _context.Books.ToListAsync();
            if (!string.IsNullOrEmpty(name))
            {
                allBooks = allBooks.Where(filter => filter.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return allBooks.Select(BookDto.FromBook);
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);
            return book is null ? null : BookDto.FromBook(book);
        }

        public async Task<BookDto> CreateBookAsync(BookRequestModel bookRequestModel)
        {
            var command = bookRequestModel.ToCommand();
            var validationErrors = _commandRunner.Validate(command, null);
            if (validationErrors.Any())
            {
                return new BookDto { ValidationErrors = validationErrors };
            }
            var id = await _commandRunner.Execute(command, null);
            return await GetBookByIdAsync(id);
        }
    }
}
