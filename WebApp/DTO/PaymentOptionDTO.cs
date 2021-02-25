using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.DTO {
    public class PaymentOptionDTO
    {
        public string paymentOptionID { get; set; }

        public PaymentTypeDTO paymentType { get; set; }

        public string paymentMethodID { get; set; }
    }
}
