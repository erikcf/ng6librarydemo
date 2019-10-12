﻿using System.Linq;
using Library.Models;

namespace Library.Dto
{
    public class BookDto
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Available { get; set; }

        public static BookDto FromBook(Book book)
        {
            return new BookDto
            {
                BookId = book.BookId,
                Available = book.Loans.All(loan => !loan.Active) || !book.Loans.Any(),
                BookName = book.Name,
                UserId = book.Loans.FirstOrDefault(loan => loan.Active)?.UserId,
                FirstName = book.Loans.FirstOrDefault(loan => loan.Active)?.User.FirstName,
                LastName = book.Loans.FirstOrDefault(loan => loan.Active)?.User.LastName
            };
        }
    }
}
