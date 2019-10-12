using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Dto;
using Library.Models;

namespace Library.Services
{
    public interface ILoanService
    {
        Task<Loan> GetLoanByIdAsync(int id);
        Task<IList<Loan>> GetLoansForBookByIdAsync(int id);
        Task<IList<Loan>> GetLoansByUserIdAsync(int id);
    }
}
