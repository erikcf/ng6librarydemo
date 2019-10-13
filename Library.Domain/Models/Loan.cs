using System;

namespace Library.Domain.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Finished { get; set; }
        public virtual Book Book { get; set; }
        public int BookId { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}
