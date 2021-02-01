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

        public async Task<IEnumerable<Reservation>> GetReservations() {
            return await GetAll().ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetSpecificReservation()
        {
            return await GetAll().Where(r => r.startTime <= DateTime.Now && r.endTime >= DateTime.Now).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetSpecificReservationByDates(DateTime leaveHour, DateTime entryHour) {
            return await GetAll().Where(r => r.startTime <= leaveHour && r.endTime >= entryHour).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
        }

        // GET: api/Reservation/5
        public async Task<Reservation> GetReservation(string id) {
            return await GetAll().Include(p => p.ParkingSpot).FirstOrDefaultAsync(s => s.reservationID == id);
        }

        // POST: api/ParkingSpots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation) {
            await AddAsync(reservation);
            return reservation;
        }
    }
}
