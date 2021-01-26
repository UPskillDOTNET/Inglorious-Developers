using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateParkAPI.Models
{
    public class ParkingSpot
    {
        public string parkingSpotID { get; set; }
        public decimal priceHour { get; set; }
        public int floor { get; set; }

        public int parkingLotID { get; set; }

        public ParkingLot ParkingLot { get; set; }

    }
}
