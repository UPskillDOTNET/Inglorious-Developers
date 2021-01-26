﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateParkAPI.Models
{
    public class ParkingLot
    {
        public int parkingLotID { get; set; }
        public string name { get; set; }
        public string companyOwner { get; set; }
        public string location { get; set; }
        public int capacity { get; set; }
        public DateTime openingTime { get; set; }
        public DateTime closingTime { get; set; }


        public List<ParkingSpot> ParkingSpots { get; set; }
    }
}