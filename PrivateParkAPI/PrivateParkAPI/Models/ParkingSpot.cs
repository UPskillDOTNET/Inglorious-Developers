﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateParkAPI.Models
{
    public class ParkingSpot
    {
        [Key]
        public string parkingSpotID { get; set; }
        public decimal priceHour { get; set; }
#nullable enable
        public int floor { get; set; }
#nullable disable
        [ForeignKey("ParkingLot")]
        public int parkingLotID { get; set; }
        public ParkingLot ParkingLot { get; set; }

    }
}
