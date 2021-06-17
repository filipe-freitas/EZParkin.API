using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EZParkin.API.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsEmailVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public IList<Vehicle> Vehicle { get; set; } = new List<Vehicle>();
    }
}
