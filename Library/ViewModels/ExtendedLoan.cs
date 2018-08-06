using System;

namespace Library.ViewModels
{
    public class ExtendedLoan
    {
        public int LoanId { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Finished { get; set; }
        public string BookName { get; set; }
        public int BookId { get; set; }
        public string FullName { get; set; }
    }
}
