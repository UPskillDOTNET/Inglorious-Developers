using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CentralAPI.Models;

namespace CentralAPI.Models
{
    public class Wallet
    {
        [Key]
        public string walletID { get; set; }

        public decimal totalAmount { get; set; }

        public string currency { get; set; }

        [ForeignKey("User")]
        public string userID;

        public User User;
    }
}
