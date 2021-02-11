using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.DTO
{
    public class ReservationPaymentDTO
    {
        public string reservationID { get; set; }

        public decimal finalPrice { get; set; }

        public string userID { get; set; }
    }
}
