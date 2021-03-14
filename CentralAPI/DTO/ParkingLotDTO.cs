using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.DTO
{
    public class ParkingLotDTO
    {
        public int parkingLotID { get; set; }
        public string name { get; set; }
        public string owner { get; set; }
        public string location { get; set; }
        public int capacity { get; set; }
        public DateTime openingTime { get; set; }
        public DateTime closingTime { get; set; }
        public string myURL { get; set; }
        public string imageURL { get; set; }
        public string managerID { get; set; }
    }
}
