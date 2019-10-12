using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Dto;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryContext _context;

        public BookService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IList<Book>> GetAllBooks(string name)
        {
            var allBooks = await _context.Books
                .Include(b => b.Loans)
                .ThenInclude(l => l.User)
                .ToListAsync();
            if (!string.IsNullOrEmpty(name))
            {
                allBooks = allBooks.Where(filter => filter.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return allBooks;
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _context.Books
                .Include(b => b.Loans)
                .ThenInclude(l => l.User)
                .FirstOrDefaultAsync(b => b.BookId == id);
        }
    }
}
