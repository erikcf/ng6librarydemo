using System.Threading.Tasks;
using Library.Dtos;
using Library.RequestModels;
using Library.Results;

namespace Library.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUserAsync(string email, string password);
        Task<UserResult> CreateUserAsync(UserRequestModel userRequestModel);
        Task<UserDto> GetUserByIdAsync(int id);
    }
}
