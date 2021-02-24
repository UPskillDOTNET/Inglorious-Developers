using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DTO;

namespace WebApp.DTO
{
    public class WebApp_ReservationDTO
    {
        public string centralReservationID { get; set; }

        public string reservationID { get; set; }

        public int parkingLotID { get; set; }
        
        public bool isCancelled { get; set; }
        
        public bool forSublet { get; set; }
        
        public DateTime startTime { get; set; }
        
        public int hours { get; set; }
        
        public DateTime endTime { get; set; }
        
        public decimal finalPrice { get; set; }
        
        public string parkingSpotID { get; set; }

        public string userID { get; set; }
        
        public WebApp_UserDTO User { get; set; }

        public WebApp_ParkingLotDTO ParkingLot { get; set; }
    }
}
