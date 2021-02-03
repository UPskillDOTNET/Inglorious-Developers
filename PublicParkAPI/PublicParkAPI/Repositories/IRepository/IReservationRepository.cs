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
        Task<IEnumerable<Reservation>> GetReservationDateTimeNow();
        Task<IEnumerable<Reservation>> GetReservationsNotCancelled();
        Task<IEnumerable<Reservation>> GetSpecificReservation(DateTime leaveHour, DateTime entryHour);
        Task<Reservation> GetReservation(string id);
        Task<bool> FindReservationAny(string id);
        Task<Reservation> PatchReservation(string id);
        Task<Reservation> PostReservation(Reservation reservation);
    }
}
