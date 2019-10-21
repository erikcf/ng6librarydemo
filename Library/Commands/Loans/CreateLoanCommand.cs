using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Domain.Models;
using Library.Helpers;

namespace Library.Commands.Loans
{
    public class CreateLoanCommand : ICommand<Loan, int>
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int Result { get; private set; }
        public async Task Execute(Loan nullEntity, LibraryContext context)
        {
            var loan = new Loan
            {
                Active = true,
                Created = DateTime.Now,
                Book = context.Books.Find(BookId),
                User = context.Users.Find(UserId)
            };
            await context.Loans.AddAsync(loan);
            await context.SaveChangesAsync();
            Result = loan.LoanId;
        }

        public IList<string> Validate(Loan nullEntity, LibraryContext context)
        {
            var errors = new List<string>();
            errors.AddError(nameof(UserId));
            errors.AddError(nameof(BookId));
            if (!context.Books.Any(book => book.BookId == BookId))
            {
                errors.Add($"Book not found with id {BookId}");
            }
            if (!context.Users.Any(book => book.UserId == UserId))
            {
                errors.Add($"Book not found with id {UserId}");
            }
            return errors;
        }
    }
}
