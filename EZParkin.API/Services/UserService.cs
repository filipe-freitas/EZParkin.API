using EZParkin.API.Domain.Models;
using EZParkin.API.Domain.Repositories;
using EZParkin.API.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EZParkin.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _userRepository.ListAsync();
        }

        public async Task<User> CreateAsync(User user)
        {
            var userAlreadyExists = _userRepository.Get(user.Email);
            if (userAlreadyExists != null) throw new Exception("E-mail already used. Try another one.");

            var createdUser = await _userRepository.CreateAsync(user);
            return createdUser;
        }
    }
}
