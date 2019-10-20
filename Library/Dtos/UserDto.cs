using System.Collections.Generic;
using System.Linq;
using Library.Domain.Models;

namespace Library.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IEnumerable<LoanDto> Loans { get; set; }

        public static UserDto FromUser(User book)
        {
            return new UserDto
            {
                UserId = book.UserId,
                FirstName = book.FirstName,
                LastName = book.LastName,
                Email = book.Email,
                Loans = book.Loans?.Select(LoanDto.FromLoanDto)
            };
        }
    }
}
