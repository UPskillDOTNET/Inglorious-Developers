using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrivateParkAPI.Repositories.IRepository;
using PrivateParkAPI.Data;
using PrivateParkAPI.Models;
using Microsoft.EntityFrameworkCore;
using PrivateParkAPI.DTO;

namespace PrivateParkAPI.Repositories.Repository
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(PrivateParkContext privateParkContext) : base(privateParkContext)
        {
        }


        public async Task<IEnumerable<Reservation>> GetReservations() {
            return await GetAll().ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetSpecificReservation()
        {
            return await GetAll().Where(r => r.startTime <= DateTime.Now && r.endTime >= DateTime.Now).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
        }

        //[Authorize]
        //[Route("api/reservations")]
        //[ApiController]
        //public class ReservationsController : ControllerBase {
        //    private readonly PrivateParkContext _context;

        //    public ReservationsController(PrivateParkContext context) {
        //        _context = context;
        //    }



        //    // GET: api/Reservations/5
        //    [HttpGet("{id}")]
        //    public async Task<ActionResult<Reservation>> GetReservation(string id) {
        //        var reservation = await _context.Reservations.Include(s => s.ParkingSpot).ThenInclude(p => p.ParkingLot).FirstOrDefaultAsync(r => r.reservationID == id);

        //        if (reservation == null) {
        //            return NotFound("Reservation does not Exist");
        //        }

        //        return reservation;
        //    }

        //    // PUT: api/Reservations/5
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPut("{id}")]
        //    public async Task<IActionResult> PutReservation(string id, Reservation reservation) {
        //        if (id != reservation.reservationID || !ModelState.IsValid) {
        //            return BadRequest(ModelState);
        //        }

        //        var parkingspot = await _context.ParkingSpots.Where(p => p.parkingSpotID == reservation.parkingSpotID).FirstOrDefaultAsync();

        //        if (parkingspot.isPrivate == true) {
        //            return Conflict("Can't make a reservation on a private parking spot");
        //        }

        //        reservation = new Reservation {
        //            reservationID = reservation.reservationID,
        //            startTime = reservation.startTime,
        //            hours = reservation.hours,
        //            endTime = reservation.startTime.AddHours(reservation.hours),
        //            finalPrice = reservation.hours * parkingspot.priceHour,
        //            parkingSpotID = reservation.parkingSpotID
        //        };

        //        _context.Entry(reservation).State = EntityState.Modified;

        //        try {
        //            await _context.SaveChangesAsync();
        //        } catch (DbUpdateConcurrencyException) {
        //            if (!ReservationExists(id)) {
        //                return NotFound("Can't Update a Reservation that does not Exist");
        //            } else {
        //                throw;
        //            }
        //        }

        //        return NoContent();
        //    }

        //    // POST: api/Reservations
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPost]
        //    public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation) {
        //        if (!ModelState.IsValid) {
        //            return BadRequest(ModelState);
        //        }

        //        var parkingspot = await _context.ParkingSpots.Where(p => p.parkingSpotID == reservation.parkingSpotID).FirstOrDefaultAsync();

        //        if (parkingspot.isPrivate == true) {
        //            return Conflict("Can't make a reservation on a private parking spot");
        //        }

        //        reservation = new Reservation {
        //            reservationID = reservation.reservationID,
        //            startTime = reservation.startTime,
        //            hours = reservation.hours,
        //            endTime = reservation.startTime.AddHours(reservation.hours),
        //            finalPrice = reservation.hours * parkingspot.priceHour,
        //            parkingSpotID = reservation.parkingSpotID
        //        };

        //        _context.Reservations.Add(reservation);

        //        try {
        //            await _context.SaveChangesAsync();
        //        } catch (DbUpdateException) {
        //            if (ReservationExists(reservation.reservationID)) {
        //                return Conflict("Reservation already Exist!");
        //            } else {
        //                throw;
        //            }
        //        }

        //        return CreatedAtAction("GetReservation", new { id = reservation.reservationID }, reservation);
        //    }

        //    // DELETE: api/Reservations/5
        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> DeleteReservation(string id) {
        //        var reservation = await _context.Reservations.FindAsync(id);
        //        if (reservation == null) {
        //            return NotFound("Can't delete a Reservation that does not Exist");
        //        }

        //        _context.Reservations.Remove(reservation);
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }

        //    private bool ReservationExists(string id) {
        //        return _context.Reservations.Any(e => e.reservationID == id);
        //    }
        //}
    }
}
