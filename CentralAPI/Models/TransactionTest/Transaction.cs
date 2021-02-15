using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Models.TransactionTest
{
    public class Transaction
    {
        public string id { get; set; }

        [ForeignKey("UserId")]
        public string userid { get; set; }

        public operation operation { get; set; }
    }

    public enum operation {

        CreateUser,
        ReservationPayment,
        CancelPayment,
    }
}
