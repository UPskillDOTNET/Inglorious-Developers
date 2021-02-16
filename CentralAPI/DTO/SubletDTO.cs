using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.DTO
{
    public class SubletDTO:CentralReservationDTO
    {
        public string subletID { get; set; }
        public string subUserID { get; set; }
        public decimal letPrice { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}
