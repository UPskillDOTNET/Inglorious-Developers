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

        [ForeignKey("Reservation")]
        public string reservationID { get; set; }

        public Reservation Reservation { get; set; }
    }
}
