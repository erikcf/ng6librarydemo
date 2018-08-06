using System;
using System.Linq;
using Library.Logic;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    public class BookDataController : Controller
    {
        private readonly LibraryContext _context;

        public BookDataController(LibraryContext context)
        {
            _context = context;
        }

        [HttpPost("[action]")]
        public IActionResult CreateBook([FromBody] Book book)
        {
            if(!ModelState.IsValid) { return BadRequest(); }
            _context.Books.Add(book);
            _context.SaveChanges();
            return Created("GetBookById", book.BookId);
        }

        [HttpGet("[action]")]
        public IActionResult GetAllBooks([FromQuery] string name)
        {
            var allBooks = _context.Books.Include(b => b.Loans).ThenInclude(l => l.User).ToList();
            if (!string.IsNullOrEmpty(name))
            {
                allBooks = allBooks.Where(filter => filter.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return Ok(LibraryHelper.GetExtendedBooks(allBooks));
        }

        [HttpGet("[action]/{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.BookId == id);
            if (book == null || !ModelState.IsValid) { return BadRequest(); }
            return Ok(book);
        }

        [HttpGet("[action]/{id}")]
        public IActionResult GetLoansForBookById(int id) => Ok(LibraryHelper.GetExtendedLoans(_context.Loans.Include(b => b.Book).Include(l => l.User)
            .Where(b => b.BookId == id).OrderByDescending(loan => loan.Active).ToList()));

        [HttpPost("[action]")]
        public IActionResult CreateLoan([FromQuery] int userId, [FromQuery] int bookId)
        {
            if(userId == 0 || bookId == 0) { return BadRequest(); }
            var addedLoan = new Loan
            {
                Active = true,
                Created = DateTime.Now,
                BookId = bookId,
                UserId = userId
            };
            _context.Loans.Add(addedLoan);
            _context.SaveChanges();
            return Created("GetLoanById", addedLoan.LoanId);
        }

        [HttpPut("[action]/{id}")]
        public IActionResult ReturnLoan(int id)
        {
            var returnedLoan = _context.Loans.FirstOrDefault(loan => loan.LoanId == id);
            if(returnedLoan == null) { return BadRequest(); }
            returnedLoan.Active = false;
            returnedLoan.Finished = DateTime.Now;
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("[action]")]
        public IActionResult LogInUser([FromQuery] string email, [FromQuery] string password) =>
            Ok(_context.Users.FirstOrDefault(user => user.Email == email && user.Password == password));

        [HttpGet("[action]/{id}")]
        public IActionResult GetLoansByUserId(int id) => Ok(LibraryHelper.GetExtendedLoans(
            _context.Loans.Where(user => user.UserId == id).Include(b => b.Book).Include(u => u.User).OrderByDescending(act => act.Active).ThenByDescending(date => date.Finished).ToList()));

        [HttpPost("[action]")]
        public IActionResult CreateUser([FromBody] User user) {
            if(!ModelState.IsValid) { return BadRequest(); }
            _context.Users.Add(user);
            _context.SaveChanges();
            return Created("GetUserById", user.UserId);
        }
    }
}
