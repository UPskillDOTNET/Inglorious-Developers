﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Models;

namespace PrivateParkAPI.Services.IServices
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDTO>> GetReservations();

        
    }
}