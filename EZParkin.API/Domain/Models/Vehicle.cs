using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EZParkin.API.Domain.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string LicensePlate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}