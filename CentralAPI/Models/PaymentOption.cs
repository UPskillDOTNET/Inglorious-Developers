using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Models
{
    public class PaymentOption
    {
        public string paymentOptionID { get; set; }

        public PaymentType paymentType { get; set; }

        [ForeignKey("PaymentMethod")]
        public string paymentMethodID { get; set; }
    }
}
