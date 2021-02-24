using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.DTO
{
    public class WebApp_ParkingSpotDTO
    {
        public string parkingSpotID { get; set; }
        public decimal priceHour { get; set; }
        public int floor { get; set; }
        public bool isPrivate { get; set; }
        public bool isCovered { get; set; }
        public int parkingLotID { get; set; }
    }
}
