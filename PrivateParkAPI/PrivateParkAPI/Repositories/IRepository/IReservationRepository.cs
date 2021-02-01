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
        Task<IEnumerable<Reservation>> GetSpecificReservation();
        Task<Reservation> GetReservation(string ID);
        Task<ActionResult<Reservation>> PostReservation(Reservation reservation);
        Task<ActionResult<Reservation>> PutReservation(string id, Reservation reservation);
        Task<ActionResult<Reservation>> DeleteReservation(string id);
    }
}
