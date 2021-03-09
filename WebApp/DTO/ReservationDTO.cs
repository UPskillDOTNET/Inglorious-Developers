using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DTO;

namespace WebApp.DTO
{
    public class ReservationDTO
    {
        public string centralReservationID { get; set; }

        public string reservationID { get; set; }

        public int parkingLotID { get; set; }
        
        public bool isCancelled { get; set; }
        
        public bool forSublet { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime startTime { get; set; }
        
        public int hours { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime endTime { get; set; }
        
        public decimal finalPrice { get; set; }
        
        public string parkingSpotID { get; set; }

        public string userID { get; set; }
        public UserDTO User { get; set; }

        public ParkingLotDTO ParkingLot { get; set; }
    }
}
