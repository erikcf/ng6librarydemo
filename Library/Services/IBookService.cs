using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Dtos;
using Library.RequestModels;
using Library.Results;

namespace Library.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync(string name);
        Task<BookDto> GetBookByIdAsync(int id);
        Task<BookResult> CreateBookAsync(BookRequestModel bookRequestModel);
    }
}
