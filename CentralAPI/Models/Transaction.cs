﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralAPI.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string transactionID { get; set; }

        public string operation { get; set; }

        [ForeignKey("User")]
        public string userID { get; set; }

        public User User { get; set; }

        public decimal value { get; set; }

        public DateTime transactionDate { get; set; }
    }
}
