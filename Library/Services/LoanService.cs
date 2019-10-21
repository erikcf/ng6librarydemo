using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Commands;
using Library.Domain.Models;
using Library.Dtos;
using Library.RequestModels;
using Library.Results;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class LoanService : ILoanService
    {
        private readonly LibraryContext _context;
        private readonly ICommandRunner _commandRunner;

        public LoanService(LibraryContext context, ICommandRunner commandRunner)
        {
            _context = context;
            _commandRunner = commandRunner;
        }

        public async Task<LoanDto> GetLoanByIdAsync(int id)
        {
            var loanEntity = await _context.Loans.FirstOrDefaultAsync(loan => loan.LoanId == id);
            return LoanDto.FromLoanDto(loanEntity);
        }

        public async Task<IEnumerable<LoanDto>> GetLoansForBookByIdAsync(int id)
        {
            var loans = await _context.Loans
                .Where(b => b.BookId == id)
                .OrderByDescending(loan => loan.Active)
                .ToListAsync();
            return loans.Select(LoanDto.FromLoanDto);
        }

        public async Task<IEnumerable<LoanDto>> GetLoansByUserIdAsync(int id)
        {
            var loans = await _context.Loans
                .Where(user => user.UserId == id)
                .OrderByDescending(act => act.Active)
                .ThenByDescending(date => date.Finished)
                .ToListAsync();
            return loans.Select(LoanDto.FromLoanDto);
        }

        public async Task<LoanResult> CreateLoanAsync(CreateLoanRequestModel createLoanRequestModel)
        {
            var command = createLoanRequestModel.ToCommand();
            var validationErrors = _commandRunner.Validate(command, null);
            if (validationErrors.Any())
            {
                return new LoanResult { ValidationErrors = validationErrors };
            }
            var id = await _commandRunner.Execute(command, null);
            var loanDto = await GetLoanByIdAsync(id);
            return new LoanResult { LoanDto = loanDto };
        }

        public async Task<LoanResult> UpdateLoanAsync(int id, UpdateLoanRequestModel updateLoanRequestModel)
        {
            var loan = await _context.Loans.FirstOrDefaultAsync(loan => loan.LoanId == id);
            if (loan is null) { return null; }

            var command = updateLoanRequestModel.ToCommand();
            var validationErrors = _commandRunner.Validate(command, loan);
            if (validationErrors.Any())
            {
                return new LoanResult { ValidationErrors = validationErrors };
            }
            await _commandRunner.Execute(command, loan);
            var loanDto = LoanDto.FromLoanDto(loan);
            return new LoanResult { LoanDto = loanDto };
        }
    }
}
