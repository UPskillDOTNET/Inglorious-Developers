using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Models
{
    public class Sublet : CentralReservation
    {
        public string subletID { get; set; }
        public string mainUserID { get; set; }
        public string subUserID { get; set; }

    }
}
