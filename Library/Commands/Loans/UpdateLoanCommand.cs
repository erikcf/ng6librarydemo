using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Domain.Models;

namespace Library.Commands.Loans
{
    public class UpdateLoanCommand : ICommand<Loan>
    {
        public bool? Active { get; set; }
        public async Task Execute(Loan loan, LibraryContext context)
        {
            if (Active.HasValue)
            {
                loan.Active = false;
                loan.Finished = DateTime.Now;
                await context.SaveChangesAsync();
            }
        }

        public IList<string> Validate(Loan loanDto, LibraryContext context)
        {
            return new List<string>();
        }
    }
}
