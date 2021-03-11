using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.DTO
{
    public class WalletDTO
    {
        public string walletID { get; set; }

        [DisplayName("Balance")]
        public decimal totalAmount { get; set; }

        public string currency { get; set; }
    }
}
