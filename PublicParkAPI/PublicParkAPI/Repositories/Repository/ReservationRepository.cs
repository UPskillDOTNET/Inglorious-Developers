using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicParkAPI.Contracts;
using PublicParkAPI.Data;
using PublicParkAPI.Models;
using PublicParkAPI.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(PublicParkContext publicParkContext) : base(publicParkContext)
        {
        }

        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<Reservation> GetReservation(string id)
        {
            return await GetAll().Include(s => s.ParkingSpot).ThenInclude(p => p.ParkingLot).FirstOrDefaultAsync(r => r.reservationID == id);
        }

        public async Task<IEnumerable<Reservation>> GetSpecificReservation()
        {
            return await GetAll().Where(r => r.startTime <= DateTime.Now && r.endTime >= DateTime.Now).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetSpecificReservationByDates(DateTime leaveHour, DateTime entryHour)
        {
            return await GetAll().Where(r => r.startTime <= leaveHour && r.endTime >= entryHour).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
        }

        public async Task<ActionResult<Reservation>> PutReservation(string id, Reservation reservation)
        {

            GetAll().Where(r => r.reservationID == id).Include(s => s.ParkingSpot);
            await UpdateAsync(reservation);
            return reservation;
        }

        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            await AddAsync(reservation);
            return reservation;
        }

        public async Task<ActionResult<Reservation>> DeleteReservation(string id)
        {
            var reservation = GetAll().FirstOrDefault(r => r.reservationID == id);
            await DeleteAsync(reservation);
            return reservation;
        }
    }
}
