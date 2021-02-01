using System;
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
<<<<<<< HEAD
        
=======
        Task<ActionResult<Reservation>> PostReservation(Reservation reservation);
        Task<ActionResult<Reservation>> PutReservation(string id, Reservation reservation);
        Task<ActionResult<Reservation>> DeleteReservation(string id);
>>>>>>> c24a4c75303b5624b4103e57ecc650f5b38aab24
    }
}
