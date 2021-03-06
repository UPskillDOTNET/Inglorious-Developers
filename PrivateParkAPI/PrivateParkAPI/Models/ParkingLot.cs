﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PrivateParkAPI.Models
{
    public class ParkingLot
    {
        [Key]
        public int parkingLotID { get; set; }
        [Required]
        [RegularExpression(@"^([a-zA-Z]\w{3,28})+(\s([a-zA-Z]\w{2,28})+(\s([a-zA-Z]\w{3,28})+)*$", ErrorMessage = "string length must be greater than 3 characters")]
        public string name { get; set; }
        [Required]
        [RegularExpression(@"^([a-zA-Z]\w{3,28})+(\s([a-zA-Z]\w{2,28})+(\s([a-zA-Z]\w{3,28})+)*$", ErrorMessage = "string length must be greater than 3 characters")]
        public string companyOwner { get; set; }
        [Required]
        [StringLength(220,MinimumLength = 3)]
        public string location { get; set; }
        [Required]
        [MinLength(1)]
        public int capacity { get; set; }
        [Required]
        public DateTime openingTime { get; set; }
        [Required]
        public DateTime closingTime { get; set; }

    }
}
