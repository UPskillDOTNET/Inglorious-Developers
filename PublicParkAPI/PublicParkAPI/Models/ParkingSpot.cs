using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Models
{
    public class ParkingSpot
    {
        public string parkingSpotID { get; set; }
        public decimal priceHour { get; set; }

        public ParkingLot ParkingLot { get; set; }
    }
}
