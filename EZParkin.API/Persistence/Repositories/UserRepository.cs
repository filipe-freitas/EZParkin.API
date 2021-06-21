using EZParkin.API.Domain.Models;
using EZParkin.API.Domain.Repositories;
using EZParkin.API.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EZParkin.API.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> CreateAsync(User user)
        {
            var userAlreadyExists = _context.Users.Where(w => w.Email == user.Email).FirstOrDefault();
            if (userAlreadyExists != null) return userAlreadyExists;

            await _context.Users.AddAsync(user);

            return user;
        }
    }
}
