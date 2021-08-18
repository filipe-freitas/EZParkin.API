using EZParkin.API.Domain.Models;
using EZParkin.API.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EZParkin.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<User> CreateAsync(User user)
        {
            return await _userService.CreateAsync(user);
        }

        [HttpGet("{email}")]
        public User Get(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new System.ArgumentNullException(email, "E-mail address not provided.");
            }

            return _userService.Get(email);
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = await _userService.ListAsync();
            return users;
        }
    }
}
