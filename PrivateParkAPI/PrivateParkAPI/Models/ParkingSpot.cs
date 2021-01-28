using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrivateParkAPI.Models
{
    public class ParkingSpot
    {
        [Key]
        public string parkingSpotID { get; set; }
        [Required]
        [Range(0.01,99.9)]
        public decimal priceHour { get; set; }
#nullable enable
        [Range(-5,9)]
        public int? floor { get; set; }
#nullable disable
        [Required]
        public bool isPrivate { get; set; }
        [ForeignKey("ParkingLot")]
        public int parkingLotID { get; set; }
        public ParkingLot ParkingLot { get; set; }

    }
}
