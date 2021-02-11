using PrivateParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrivateParkAPI.Repositories.IRepository
{
    public interface IReservationRepository : IBaseRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetReservations();
        Task<IEnumerable<Reservation>> GetReservationDateTimeNow();
        Task<IEnumerable<Reservation>> GetReservationsNotCancelled();
        Task<IEnumerable<Reservation>> GetSpecificReservation(DateTime startDate, DateTime endDate);
        Task<Reservation> GetReservation(string ID);
        Task<bool> FindReservationAny(string id);
        Task<Reservation> PostReservation(Reservation reservation);
        Task<Reservation> PatchReservation(Reservation reservation);
    }
}
