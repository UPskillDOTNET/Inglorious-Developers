using Microsoft.AspNetCore.Mvc;
using PublicParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Contracts
{
    public interface IReservationRepository : IBaseRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetReservations();
        Task<IEnumerable<Reservation>> GetSpecificReservation();
        Task<IEnumerable<Reservation>> GetSpecificReservationByDates(DateTime leaveHour, DateTime entryHour);
        Task<Reservation> GetReservation(string id);
        //Task<ActionResult<Reservation>> PutReservation(string id, Reservation reservation);
        Task<ActionResult<Reservation>> PatchReservation(string id);
        Task<ActionResult<Reservation>> PostReservation(Reservation reservation);
    }
}
