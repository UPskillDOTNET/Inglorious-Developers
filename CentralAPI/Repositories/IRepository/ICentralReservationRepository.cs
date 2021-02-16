using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CentralAPI.Models;
using CentralAPI.DTO;

namespace CentralAPI.Repositories.IRepository {
    public interface ICentralReservationRepository : IBaseRepository<CentralReservation> {
        Task<IEnumerable<CentralReservation>> GetAllCentralReservations();
        Task<IEnumerable<CentralReservation>> GetCentralReservationDateTimeNow();
        Task<IEnumerable<CentralReservation>> GetCentralReservationsNotCancelled();
        Task<IEnumerable<CentralReservation>> GetSpecificCentralReservation(DateTime leaveHour, DateTime entryHour);
        Task<bool> FindCentralReservationAny(string id);
        Task<bool> subletReservationAny(string pid, int PID, DateTime startTime);
        Task<CentralReservation> GetCentralReservationById(string id);
        Task<CentralReservation> PatchCentralReservation(CentralReservation reservation);
        Task<CentralReservation> PostCentralReservation(CentralReservation centralReservation);
        Task<CentralReservation> GetsubletReservation(string pid, int PID, DateTime startTime);
    }
}