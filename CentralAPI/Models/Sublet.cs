using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Models
{
    public class Sublet
    {
        [Key]
        public string subletID { get; set; }
        [ForeignKey("Reservation")]
        public string reservationID { get; set; }
        [ForeignKey("User")]
        public string mainUserID { get; set; }
        [ForeignKey("User")]
        public string subUserID { get; set; }
        [Range(00.00,99.99)]
        public decimal letPrice { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

    }
}
