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
        public string paymentMethodId;

        public string name;

        [ForeignKey("User")]
        public string userId;
    }
}
