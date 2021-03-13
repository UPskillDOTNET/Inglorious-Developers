using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO
{
    public class ParkingLotDTO
    {
        public int parkingLotID { get; set; }
        [DisplayName("Name")]
        public string name { get; set; }
        [DisplayName("Owner")]
        public string owner { get; set; }
        [DisplayName("Location")]
        public string location { get; set; }
        [DisplayName("Capacity")]
        public int capacity { get; set; }
        [DisplayName("Opening Time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm:ss}")]
        public DateTime openingTime { get; set; }
        [DisplayName("Closing Time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm:ss}")]
        public DateTime closingTime { get; set; }

        public string myURL { get; set; }
        public string imageURL { get; set; }
    }
}
