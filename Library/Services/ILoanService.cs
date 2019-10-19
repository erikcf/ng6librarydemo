using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Dtos;
using Library.RequestModels;

namespace Library.Services
{
    public interface ILoanService
    {
        Task<LoanDto> GetLoanByIdAsync(int id);
        Task<IEnumerable<LoanDto>> GetLoansForBookByIdAsync(int id);
        Task<IEnumerable<LoanDto>> GetLoansByUserIdAsync(int id);
        Task<LoanDto> CreateLoanAsync(CreateLoanRequestModel createLoanRequestModel);
        Task<LoanDto> UpdateLoanAsync(int id, UpdateLoanRequestModel updateLoanRequestModel);
    }
}
