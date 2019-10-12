using System.Threading.Tasks;
using Library.Models;

namespace Library.Services
{
    public interface IUserService
    {
        Task<User> GetUserAsync(string email, string password);
    }
}
