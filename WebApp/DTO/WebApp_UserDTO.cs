﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.DTO
{
    public class WebApp_UserDTO
    {
        public string userID { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public string nif { get; set; }

        public string paymentMethodID { get; set; }
    }
}
