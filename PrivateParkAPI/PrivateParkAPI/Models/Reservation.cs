﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrivateParkAPI.Models {
    public class Reservation {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string reservationID { get; set; }
        [Required]
        public bool isCancelled { get; set; }
        [Required]
        public DateTime startTime { get; set; }
        [Required]
        public int hours { get; set; }
        public decimal finalPrice { get; set; }
        public DateTime endTime { get; set; }

        [ForeignKey("ParkingSpot")]
        public string parkingSpotID { get; set; }
        public ParkingSpot ParkingSpot { get; set; }
    }
}
