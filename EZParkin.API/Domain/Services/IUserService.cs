﻿using EZParkin.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        void Delete(int userId);
    }
}
