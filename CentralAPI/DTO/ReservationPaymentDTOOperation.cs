﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.DTO
{
    public class ReservationPaymentDTOOperation : ReservationPaymentDTO
    {
        public string message { get; set; }

        public int operation { get; set; }

        public bool isSuccess { get; set; }
    }
}
