using System.Collections.Generic;

namespace Library.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public List<Loan> Loans { get; set; }
    }
}
