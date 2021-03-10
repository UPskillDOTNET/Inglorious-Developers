using System;

namespace WebApp.DTO {
    public class PaymentDTO
    {
        public string paymentID { get; set; }

        public string reservationID { get; set; }

        public decimal finalPrice { get; set; }
        
        public string userID { get; set; }
        
        public DateTime timeStamp { get; set; }
        
        public string paymentMethod { get; set; }
    }
}
