using PublicParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicParkAPI.Contracts
{
    public interface IReservationRepository : IBaseRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetReservations();
        Task<IEnumerable<Reservation>> GetReservationDateTimeNow();
        Task<IEnumerable<Reservation>> GetReservationsNotCancelled();
        Task<IEnumerable<Reservation>> GetSpecificReservation(DateTime leaveHour, DateTime entryHour);
        Task<bool> FindReservationAny(string id);
        Task<Reservation> GetReservation(string id);
        Task<Reservation> PatchReservation(Reservation reservation);
        Task<Reservation> PostReservation(Reservation reservation);
    }
}
