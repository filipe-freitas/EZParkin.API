using EZParkin.API.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EZParkin.API.Domain.Services
{
    public interface IUserService
    {
        Task<User> CreateAsync(User user);

        User Get(string userEmail);

        User Get(int userId);

        Task<IEnumerable<User>> ListAsync();

        Task<User> UpdateAsync(User user);

        Task Delete(int userId);
    }
}
