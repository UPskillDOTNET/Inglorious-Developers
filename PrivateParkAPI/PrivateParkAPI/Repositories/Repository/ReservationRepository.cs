using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrivateParkAPI.Repositories.IRepository;
using PrivateParkAPI.Data;
using PrivateParkAPI.Models;
using Microsoft.EntityFrameworkCore;
using PrivateParkAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace PrivateParkAPI.Repositories.Repository
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(PrivateParkContext privateParkContext) : base(privateParkContext)
        {
        }
        // GET: api/Reservations
        public async Task<IEnumerable<Reservation>> GetReservations() {
            return await GetAll().ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationDateTimeNow()
        {
            return await GetAll().Where(r => r.startTime <= DateTime.Now && r.endTime >= DateTime.Now).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
        }
<<<<<<< HEAD
        public async Task<IEnumerable<Reservation>> GetSpecificReservation(DateTime startDate, DateTime endDate)
        {
            return await GetAll().Where(r => (r.startTime >= startDate && r.endTime <= endDate) || (r.startTime <= endDate && r.endTime >= startDate)).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
        }
=======

        public async Task<IEnumerable<Reservation>> GetSpecificReservationByDates(DateTime leaveHour, DateTime entryHour) {
            return await GetAll().Where(r => r.startTime <= leaveHour && r.endTime >= entryHour).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
        }

>>>>>>> c24a4c75303b5624b4103e57ecc650f5b38aab24
        // GET: api/Reservation/5
        public async Task<Reservation> GetReservation(string id) {
            return await GetAll().Include(p => p.ParkingSpot).FirstOrDefaultAsync(s => s.reservationID == id);
        }

        // POST: api/ParkingSpots
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation) {
            await AddAsync(reservation);
            return reservation;
        }

        // PUT: api/ParkingSpots/5
        public async Task<ActionResult<Reservation>> PutReservation(string id, Reservation reservation) {

            GetAll().Where(r => r.reservationID == id).Include(s => s.ParkingSpot);
            await UpdateAsync(reservation);
            return reservation;
        }

        // DELETE: api/ParkingSpots/5
        public async Task<ActionResult<Reservation>> DeleteReservation(string id) {
            var reservation = GetAll().FirstOrDefault(r => r.reservationID == id);
            await DeleteAsync(reservation);
            return reservation;
        }
    }
}
