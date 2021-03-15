using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.DTO
{
    public class UserDTO
    {
        public string userID { get; set; }
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string name { get; set; }
        public string Email { get; set; }

        public string nif { get; set; }

        public string paymentMethodID { get; set; }

        public WalletDTO walletDTO { get; set; }
    }
}
