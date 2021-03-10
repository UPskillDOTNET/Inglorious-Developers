using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.DTO
{
    public class ParkingSpotDTO
    {
        public string parkingSpotID { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        [Range(0, 9999999999.999999)]
        public decimal priceHour { get; set; }
        public int floor { get; set; }
        public bool isPrivate { get; set; }
        public bool isCovered { get; set; }
        public int parkingLotID { get; set; }
    }
}
