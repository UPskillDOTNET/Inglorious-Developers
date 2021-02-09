using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CentralAPI.Models;

namespace CentralAPI.DTO {
    public class CentralReservationDTO {
        [Key]
        public string reservationID { get; set; }
        public bool isCancelled { get; set; }
        public DateTime startTime { get; set; }
        public int hours { get; set; }
        public DateTime endTime { get; set; }
        [Range(00.00, 99.99)]
        public decimal finalPrice { get; set; }
        //public string parkingSpotID { get; set; }
        [ForeignKey("User")]
        public string userID { get; set; }
        public User User { get; set; }
        [ForeignKey("ParkingLot")]
        public int parkingLotID { get; set; }
        public ParkingLot ParkingLot { get; set; }
    }
}
