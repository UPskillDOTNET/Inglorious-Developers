using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAPI.Models
{
    public class Wallet
    {
        [Key]
        public string walletID { get; set; }

        public decimal totalAmount { get; set; }

        public string currency { get; set; }

        [ForeignKey("User")]
        public string userID { get; set; }

        public User User { get; set; }


        public bool withdraw(decimal value)
        {
            if (totalAmount - value < 0)
            {
                return false;
            }
            totalAmount -= value;
            return true;
        }

        public bool deposit(decimal value)
        {
            if (value < 0)
            {
                return false;
            }
            totalAmount += value;
            return true;
        }

        public decimal balance()
        {
            return totalAmount;
        }
    }
}
