using System.Collections.Generic;

namespace Library.Domain.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
    }
}
