using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CentralAPI.Models;

namespace CentralAPI.DTO {
    public class CentralReservationDTO {   
        public string centralReservationID { get; set; }
        public string reservationID { get; set; }
        public bool isCancelled { get; set; }
        public bool forSublet { get; set; }
        public DateTime startTime { get; set; }
        public int hours { get; set; }
        public DateTime endTime { get; set; }
        public decimal finalPrice { get; set; }
        public string parkingSpotID { get; set; }
        public string userID { get; set; }
        public int parkingLotID { get; set; }
    }
}
