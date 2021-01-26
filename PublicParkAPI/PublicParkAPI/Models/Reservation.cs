using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Models
{
    public class Reservation
    {
        public string reservationID { get; set; }
        public DateTime startTime { get; set; }
        public int hours { get; set; }
        public DateTime endTime { get; set; }
        public string parkingSpotID { get; set; }
        public int parkingLotID { get; set; }
       
        public ParkingSpot ParkingSpot { get; set; }
        public ParkingLot ParkingLot { get; set; }
    }
}
