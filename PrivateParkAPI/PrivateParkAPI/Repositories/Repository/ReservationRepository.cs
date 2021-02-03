using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrivateParkAPI.Data;
using PrivateParkAPI.Models;
using PrivateParkAPI.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateParkAPI.Repositories.Repository
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(PrivateParkContext privateParkContext) : base(privateParkContext)
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
            return await GetAll().Where(r => (r.startTime <= DateTime.Now && r.endTime >= DateTime.Now)).Where(r => r.isCancelled == true).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetSpecificReservation(DateTime startDate, DateTime endDate)
        {
            return await GetAll().Where(r => (r.startTime >= startDate && r.endTime <= endDate) || (r.startTime <= endDate && r.endTime >= startDate)).Where(r => r.isCancelled == true).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
        }

        public async Task<Reservation> GetReservation(string id)
        {
            return await GetAll().Include(p => p.ParkingSpot).ThenInclude(p => p.ParkingLot).FirstOrDefaultAsync(s => s.reservationID == id);
        }

        public async Task<Reservation> PostReservation(Reservation reservation)
        {
            await AddAsync(reservation);
            return reservation;
        }

        public async Task<Reservation> PatchReservation(string id)
        {
            var x = GetAll().Include(s => s.ParkingSpot).ThenInclude(p => p.ParkingLot)
                .FirstOrDefaultAsync(r => r.reservationID == id)
                .Result;
            x.isCancelled = true;
            await UpdateAsync(x);
            return x;
            
        }
    }
}
