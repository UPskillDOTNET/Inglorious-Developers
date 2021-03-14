using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.DTO
{
    public class ParkingSpotDTO
    {
        [Display(Name = "Designation")]
        public string parkingSpotID { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        [Range(0, 9999999999.999999)]
        [Display(Name = "Price/Hour")]
        public decimal priceHour { get; set; }
        [Display(Name = "Floor")]
        public int floor { get; set; }
        [Display(Name ="Private")]
        public bool isPrivate { get; set; }
        [Display(Name = "Covered")]
        public bool isCovered { get; set; }
        public int parkingLotID { get; set; }
    }
}
