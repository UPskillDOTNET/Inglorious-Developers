using System;
using PrivateParkAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PrivateParkAPI.DTO {
    public class ParkingSpotDTO {
        public string parkingSpotID { get; set; }
        [Required]
        public decimal priceHour { get; set; }
        [Required]
        public int floor { get; set; }
        [Required]
        public bool isPrivate { get; set; }
        public bool isCovered { get; set; }
        [Required]
        public int parkingLotID { get; set; }
        public ParkingLot ParkingLot { get; set; }
    }
}
