using System;

namespace WebApp.DTO {
    public class WebApp_PaymentDTO
    {
        public string paymentID { get; set; }
        
        public decimal finalPrice { get; set; }
        
        public string userID { get; set; }
        
        public DateTime timeStamp { get; set; }
        
        public string paymentMethod { get; set; }
    }
}
