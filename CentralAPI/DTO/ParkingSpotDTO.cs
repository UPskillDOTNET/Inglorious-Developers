using System.ComponentModel.DataAnnotations;
using CentralAPI.Models;

namespace CentralAPI.DTO {
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
