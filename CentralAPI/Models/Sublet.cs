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
        public string reservationID { get; set; }
        public string mainUserID { get; set; }
        public string subUserID { get; set; }
        public decimal letPrice { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public bool isCancelled { get; set; }

    }
}
