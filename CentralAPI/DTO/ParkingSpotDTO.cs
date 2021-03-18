using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CentralAPI.DTO {
    public class ParkingSpotDTO {
        public string parkingSpotID { get; set; }
        public decimal priceHour { get; set; }
        public int floor { get; set; }
        public bool isPrivate { get; set; }
        public bool isCovered { get; set; }
    }
}