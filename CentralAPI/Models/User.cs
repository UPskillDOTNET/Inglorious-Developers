using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Models
{
    public class User
    {
        public string userID { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public string nif { get; set; }

        public string walletID { get; set; }
    }
}
