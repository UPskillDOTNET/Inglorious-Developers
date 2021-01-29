using PublicParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.DTO
{
    public class ParkingSpotDTO
    {
        public string parkingSpotID { get; set; }

        public decimal priceHour { get; set; }

        public int ParkingLotID { get; set; }

        public ParkingLot ParkingLot { get; set; }
    }
}
