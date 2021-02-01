using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.DTO
{
    public class ParkingLotDTO
    {

        public int parkingLotID { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string municipality { get; set; }
        [Required]
        public string location { get; set; }
        //[Required]
        //public int capacity { get; set; }
        //[Required]
        //public DateTime openingTime { get; set; }
        //[Required]
        //public DateTime closingTime { get; set; }
    }
}
