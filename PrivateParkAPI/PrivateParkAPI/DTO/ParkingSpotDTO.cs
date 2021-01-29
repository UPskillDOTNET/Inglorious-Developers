using System;
using PrivateParkAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateParkAPI.DTO {
    public class ParkingSpotDTO {
        public string parkingSpotID { get; set; }
        public decimal priceHour { get; set; }
        public bool isPrivate { get; set; }
        public int ParkingLotID { get; set; }
        public ParkingLot ParkingLot { get; set; }
    }
}
