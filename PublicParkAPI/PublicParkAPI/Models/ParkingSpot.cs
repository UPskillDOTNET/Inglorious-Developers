using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Models
{
    public class ParkingSpot
    {
        [Key]
        public string parkingSpotID { get; set; }
        [Required]
        [Range(0.01, 99.9)]
        public decimal priceHour { get; set; }
    }
}
