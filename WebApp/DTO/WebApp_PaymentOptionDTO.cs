using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.DTO {
    public class WebApp_PaymentOptionDTO
    {
        public string paymentOptionID { get; set; }

        public WebApp_PaymentTypeDTO paymentType { get; set; }

        public string paymentMethodID { get; set; }
    }
}
