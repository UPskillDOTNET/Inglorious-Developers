using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrivateParkAPI.Models
{
    public class Reservation
    {
        [Key]
        public string reservationID { get; set; }
        [Required]
        public DateTime startTime { get; set; }
        [Required]
        [MinLength(1)]
        public int hours { get; set; }
        public DateTime endTime { get; set; }

        [ForeignKey("ParkingSpot")]
        public string parkingSpotID { get; set; }
        public ParkingSpot ParkingSpot { get; set; }

       

       
    }
}
