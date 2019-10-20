using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Dtos;
using Library.RequestModels;
using Library.Results;

namespace Library.Services
{
    public interface ILoanService
    {
        Task<LoanDto> GetLoanByIdAsync(int id);
        Task<IEnumerable<LoanDto>> GetLoansForBookByIdAsync(int id);
        Task<IEnumerable<LoanDto>> GetLoansByUserIdAsync(int id);
        Task<LoanResult> CreateLoanAsync(CreateLoanRequestModel createLoanRequestModel);
        Task<LoanResult> UpdateLoanAsync(int id, UpdateLoanRequestModel updateLoanRequestModel);
    }
}
