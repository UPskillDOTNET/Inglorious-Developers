using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAPI.Models
{
    public class TransactionDTO
    {

        public string transactionID { get; set; }

        // Verificar isto da Operation - entre deposit e withdraw - boolean?
        public string operation { get; set; }

        public string userID { get; set; }

        public decimal value { get; set; }

        public DateTime transactionDate { get; set; }
    }
}
