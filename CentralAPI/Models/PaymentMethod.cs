using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Models
{
    public class PaymentMethod
    {
        [Key]
        public string paymentMethodID { get; set; }

        public string name { get; set; }

        public string myUrl { get; set; }

        [ForeignKey("UserID")]
        public string userID { get; set; }

        public User User { get; set; }
    }
}
