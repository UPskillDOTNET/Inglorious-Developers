using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.DTO
{
    public class ReservationDTO
    {
        public string reservationID { get; set; }
        public DateTime startTime { get; set; }
        public int hours { get; set; }
        public decimal finalPrice { get; set; }
        public DateTime endTime { get; set; }
        public bool isCancelled { get; set; }
        public string parkingSpotID { get; set; }
        public ParkingSpot ParkingSpot { get; set; }
    }
}
