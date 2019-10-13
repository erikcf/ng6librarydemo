using System;
using Library.Domain.Models;

namespace Library.Dtos
{
    public class LoanDto
    {
        public int LoanId { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Finished { get; set; }
        public string BookName { get; set; }
        public int BookId { get; set; }
        public string FullName { get; set; }


        public static LoanDto FromLoanDto(Loan loan)
        {
            return new LoanDto
            {
                LoanId = loan.LoanId,
                Active = loan.Active,
                Created = loan.Created,
                Finished = loan.Finished,
                BookName = loan.Book.Name,
                BookId = loan.BookId,
                FullName = $"{loan.User?.FirstName} {loan.User?.LastName}"
            };
        }
    }
}
