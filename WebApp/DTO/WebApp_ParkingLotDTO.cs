using System;

namespace WebApp.DTO
{
    public class WebApp_ParkingLotDTO
    {
        public int parkingLotID { get; set; }

        public string name { get; set; }

        public string owner { get; set; }

        public string location { get; set; }

        public int capacity { get; set; }

        public DateTime openingTime { get; set; }

        public DateTime closingTime { get; set; }

        public string myURL { get; set; }
    }
}
