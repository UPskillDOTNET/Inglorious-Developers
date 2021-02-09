using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAPI.Models
{
    public class Sublet
    {
        [Key]
        public string subletID { get; set; }

        public int durationSublet { get; set; }

        public decimal priceHourLet { get; set; }

        [ForeignKey("User")]
        public string userID { get; set; }

        [ForeignKey("Reservation")]
        public string reservationID { get; set; }
    }
}
