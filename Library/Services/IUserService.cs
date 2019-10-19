using System.Threading.Tasks;
using Library.Dtos;
using Library.RequestModels;

namespace Library.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUserAsync(string email, string password);
        Task<UserDto> CreateUserAsync(UserRequestModel userRequestModel);
        Task<UserDto> GetUserByIdAsync(int id);
    }
}
