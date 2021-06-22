using EZParkin.API.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EZParkin.API.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);

        User Get(int userId);

        User Get(string email);
        
        Task<IEnumerable<User>> ListAsync();

        Task<User> UpdateAsync(User user);

        void Delete(int userId);
    }
}
