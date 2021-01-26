using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Models
{
    public class ParkingSpot
    {
        public string parkingSpotID { get; set; }
        public decimal priceHour { get; set; }

        [ForeignKey("ParkingSpot")]
        public int ParkingLotID { get; set; }

        public ParkingLot ParkingLot { get; set; }
    }
}
