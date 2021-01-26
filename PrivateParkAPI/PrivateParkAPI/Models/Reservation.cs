using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrivateParkAPI.Models
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string reservationID { get; set; }
        public DateTime startTime { get; set; }
        public int hours { get; set; }
        public DateTime endTime { get; set; }

        [ForeignKey("ParkingSpot")]
        public string parkingSpotID { get; set; }

        [ForeignKey("ParkingLot")]
        public int parkingLotID { get; set; }

        public ParkingSpot ParkingSpot { get; set; }
        public ParkingLot ParkingLot { get; set; }
    }
}
