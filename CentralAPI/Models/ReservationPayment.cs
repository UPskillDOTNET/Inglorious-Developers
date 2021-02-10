using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Models
{
    public class ReservationPayment : Payment
    {
        [Key]
        public string reservationID { get; set; }
    }
}
