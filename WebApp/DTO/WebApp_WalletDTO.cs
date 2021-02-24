using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.DTO
{
    public class WebApp_WalletDTO
    {
        public string walletID { get; set; }

        public decimal totalAmount { get; set; }

        public string currency { get; set; }

        public string userID { get; set; }

        public WebApp_UserDTO User { get; set; }
    }
}
