using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Models
{
    public class Payment
    {
        public string paymentID { get; set; }

        public decimal value { get; set; }

        [ForeignKey("PaymentOption")]
        public string paymentOptionID { get; set; }

        [ForeignKey("PaymentMethod")]
        public string paymentMethodID { get; set; }

        [ForeignKey("User")]
        public string userID { get; set; }

        public DateTime timeStamp;

        public PaymentOption PaymentOption { get; set; }

        public User User { get; set; }
    }
}
