﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PrivateParkAPI.DTO;
using PrivateParkAPI.Models;

namespace PrivateParkAPI.Repositories.IRepository
{
    public interface IReservationRepository: IBaseRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetReservations();
        Task<IEnumerable<Reservation>> GetReservationDateTimeNow();
        Task<IEnumerable<Reservation>> GetSpecificReservation(DateTime startDate, DateTime endDate);
        Task<Reservation> GetReservation(string ID);
        
    }
}
