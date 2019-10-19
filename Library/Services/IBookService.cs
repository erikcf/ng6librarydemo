using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Dtos;
using Library.RequestModels;

namespace Library.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync(string name);
        Task<BookDto> GetBookByIdAsync(int id);
        Task<BookDto> CreateBookAsync(BookRequestModel bookRequestModel);
    }
}
