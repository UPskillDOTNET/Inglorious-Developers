using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Models
{
    public class OvertimePayment : Payment
    {
        public DateTime parkingEnd { get; set; }

        [ForeignKey("Reservation")]
        public string reservationID { get; set; }
    }
}
