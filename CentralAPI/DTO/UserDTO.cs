using CentralAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.DTO
{
    public class UserDTO
    {
        public string userID { get; set; }

        public string Email { get; set; }

        public string password { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
       
        public string nif { get; set; }
    }
}
