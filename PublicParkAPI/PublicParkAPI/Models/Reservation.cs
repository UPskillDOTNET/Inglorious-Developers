using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicParkAPI.Models
{
    public class Reservation
    {
        [Key]
        public string reservationID { get; set; }
        [Required]
        public DateTime startTime { get; set; }
        [Required]
        public int hours { get; set; }
        public decimal finalPrice { get; set; }
        public DateTime endTime { get; set; }      
        [ForeignKey("ParkingSpot")]
        public string parkingSpotID { get; set; }

        public ParkingSpot ParkingSpot { get; set; }
    }
}
