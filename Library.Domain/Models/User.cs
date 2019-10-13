using System.Collections.Generic;

namespace Library.Domain.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
    }
}
