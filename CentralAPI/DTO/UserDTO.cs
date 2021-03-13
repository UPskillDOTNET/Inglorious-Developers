using CentralAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.DTO
{
    public class UserDTO
    {
        public  string UserName { get; set; }
        
        public string Password { get; set; }
        public string name { get; set; }
        public  string Email { get; set; }

        public string nif { get; set; }

        public string paymentMethodID { get; set; }
    }
}
