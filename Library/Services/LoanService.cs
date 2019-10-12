﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Dto;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class LoanService : ILoanService
    {
        private readonly LibraryContext _context;

        public LoanService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Loan> GetLoanByIdAsync(int id) => await _context.Loans
            .Include(b => b.Book)
            .Include(l => l.User)
            .FirstOrDefaultAsync(loan => loan.LoanId == id);

        public async Task<IList<Loan>> GetLoansForBookByIdAsync(int id)
        {
            return await _context.Loans
                .Include(b => b.Book)
                .Include(l => l.User)
                .Where(b => b.BookId == id)
                .OrderByDescending(loan => loan.Active)
                .ToListAsync();
        }

        public async Task<IList<Loan>> GetLoansByUserIdAsync(int id)
        {
            return await _context.Loans
                .Where(user => user.UserId == id)
                .Include(b => b.Book)
                .Include(u => u.User)
                .OrderByDescending(act => act.Active)
                .ThenByDescending(date => date.Finished)
                .ToListAsync();
        }
    }
}