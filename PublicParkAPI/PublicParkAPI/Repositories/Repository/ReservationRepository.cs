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

        public async Task<IEnumerable<Reservation>> GetReservationsNotCancelled()
        {
            return await GetAll().Where(r => r.isCancelled == false).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationDateTimeNow()
        {
            return await GetAll().Where(r => r.startTime <= DateTime.Now && r.endTime >= DateTime.Now).Where(r => r.isCancelled == false).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetSpecificReservation(DateTime startDate, DateTime endDate)
        {
            return await GetAll().Where(r => (r.startTime >= startDate && r.endTime <= endDate) || (r.startTime <= endDate && r.endTime >= startDate)).Where(r => r.isCancelled == false).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
        }

        public async Task<Reservation> GetReservation(string id)
        {
            return await GetAll().Include(s => s.ParkingSpot).ThenInclude(p => p.ParkingLot).FirstOrDefaultAsync(r => r.reservationID == id);
        }
        public async Task<bool> FindReservationAny(string id)
        {
            return await GetAll().Where(p => p.reservationID == id).AnyAsync();
        }
        public async Task<Reservation> PostReservation(Reservation reservation)
        {
            reservation = await AddAsync(reservation);
            return reservation;
        }

        public async Task<Reservation> PatchReservation(Reservation reservation)
        {
            reservation = await UpdateAsync(reservation);

            return reservation;
        }

    }
}
