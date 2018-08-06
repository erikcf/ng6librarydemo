using System;

namespace Library.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Finished { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
