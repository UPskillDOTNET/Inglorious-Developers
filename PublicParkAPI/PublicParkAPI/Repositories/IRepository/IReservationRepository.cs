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
        //Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations();
        //Task<ActionResult<ReservationDTO>> GetReservation(string id);
        //Task<IActionResult> PutReservation(string id, ReservationDTO reservation);
        //Task<ActionResult<ReservationDTO>> PostReservation(ReservationDTO reservation);
        //Task<IActionResult> DeleteReservation(string id);
        //bool ReservationExists(string id);
    }
}
