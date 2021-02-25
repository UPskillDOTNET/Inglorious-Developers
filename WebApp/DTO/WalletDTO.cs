using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.DTO
{
    public class WalletDTO
    {
        public string walletID { get; set; }

        public decimal totalAmount { get; set; }

        public string currency { get; set; }

        public string userID { get; set; }

        public UserDTO User { get; set; }
    }
}
