using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateParkAPI.Models
{
    public class Reservation
    {
        public string reservationID { get; set; }
        public DateTime startTime { get; set; }
        public int hours { get; set; }
        public DateTime endTime { get; set; }

        [ForeignKey("ParkingSpot")]
        public string parkingSpotID { get; set; }

        [ForeignKey("ParkingSpot")]
        public int parkingLotID { get; set; }

        public ParkingSpot ParkingSpot { get; set; }
        public ParkingLot ParkingLot { get; set; }
    }
}
