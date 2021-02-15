using CentralAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.DTO
{
    public class PaymentDTO
    {
       
        public string paymentID { get; set; }
        public decimal finalPrice { get; set; }
        public string userID { get; set; }
        public DateTime timeStamp { get; set; }
    }
}
