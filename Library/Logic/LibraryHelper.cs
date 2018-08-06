using System.Collections.Generic;
using System.Linq;
using Library.Models;
using Library.ViewModels;

namespace Library.Logic
{
    public static class LibraryHelper
    {
        public static List<ExtendedBook> GetExtendedBooks(List<Book> allBooks)
        {
            return allBooks.Select(book => new ExtendedBook
            {
                BookId = book.BookId,
                Available = book.Loans.All(loan => !loan.Active) || !book.Loans.Any(),
                BookName = book.Name,
                UserId = book.Loans.FirstOrDefault(loan => loan.Active)?.UserId,
                FirstName = book.Loans.FirstOrDefault(loan => loan.Active)?.User.FirstName,
                LastName = book.Loans.FirstOrDefault(loan => loan.Active)?.User.LastName,
            }).ToList();
        }
        public static List<ExtendedLoan> GetExtendedLoans(List<Loan> allLoans)
        {
            return allLoans.Select(loan => new ExtendedLoan
            {
                LoanId = loan.LoanId,
                Active = loan.Active,
                Created = loan.Created,
                Finished = loan.Finished,
                BookName = loan.Book.Name,
                BookId = loan.BookId,
                FullName = $"{loan.User.FirstName} {loan.User.LastName}" 
            }).ToList();
        }
    }
}
