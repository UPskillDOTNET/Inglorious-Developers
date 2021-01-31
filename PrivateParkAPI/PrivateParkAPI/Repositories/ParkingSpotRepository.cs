using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrivateParkAPI.Contracts;
using PrivateParkAPI.Data;
using PrivateParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateParkAPI.Repositories {
    public class ParkingSpotRepository : IParkingSpotRepository {
        private readonly PrivateParkContext _context;

        public ParkingSpotRepository(PrivateParkContext context) {
            _context = context;
        }

        public IEnumerable<ParkingSpot> GetParkingSpots() {
            return _context.ParkingSpots.Include(p => p.ParkingLot).ToList();
        }


        public ParkingSpot GetParkingSpot(string id) {
            var parkingSpot = _context.ParkingSpots.Include(p => p.ParkingLot).FirstOrDefaultAsync(s => s.parkingSpotID == id);
            return parkingSpot.Result;
        }

        public IEnumerable<ParkingSpot> GetParkingFreeSpots() {
            var parkingSpots = _context.ParkingSpots.Include(p => p.ParkingLot).Where(p => !_context.Reservations.Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).Any(r => r.startTime <= DateTime.Now && r.endTime >= DateTime.Now)).ToList();
            var res = from p in _context.ParkingSpots where !(from r in _context.Reservations where r.parkingSpotID == p.parkingSpotID && (r.startTime <= DateTime.Now && r.endTime >= DateTime.Now) select r.parkingSpotID).Contains(p.parkingSpotID) select p;
            return parkingSpots;
        }

        public async Task<IEnumerable<ParkingSpot>> GetParkingSpecificFreeSpotsAsync(DateTime entryHour, DateTime leaveHour) {

            var reservation = await _context.Reservations.Where(r => r.startTime <= leaveHour && r.endTime >= entryHour).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
            var parkingSpots = await _context.ParkingSpots.Include(p => p.ParkingLot).ToListAsync();

            foreach (var r in reservation) {
                parkingSpots.Remove(r.ParkingSpot);
            }
            return parkingSpots;
        }

        public async Task<IEnumerable<ParkingSpot>> GetParkingPriceFreeSpotsAsync(decimal price) {

            var reservation = await _context.Reservations.Where(r => r.startTime <= DateTime.Now && r.endTime >= DateTime.Now).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
            var parkingSpots = await _context.ParkingSpots.Where(p => p.priceHour <= price).Include(p => p.ParkingLot).ToListAsync();

            foreach (var r in reservation) {
                parkingSpots.Remove(r.ParkingSpot);
            }
            return parkingSpots;
        }

        public bool ParkingSpotExists(string id) {
            _context.ParkingSpots.AnyAsync(q => q.parkingSpotID.Equals(id));
            return true;
        }

        public bool PostParkingSpot(ParkingSpot parkingSpot) {
            _context.ParkingSpots.Add(parkingSpot);
            _context.SaveChangesAsync();
            return true;
        }

        public bool PutParkingSpot(ParkingSpot parkingSpot) {
            _context.Entry(parkingSpot).State = EntityState.Modified;
            _context.SaveChangesAsync();
            return true;
        }

        public bool DeleteParkingSpot(string id) {
            var parkingSpot = _context.ParkingSpots.Find(id);
            _context.ParkingSpots.Remove(parkingSpot);
            _context.SaveChangesAsync();
            return true;
        }

        IEnumerable<ParkingSpot> IParkingSpotRepository.GetParkingSpecificFreeSpotsAsync(DateTime entryHour, DateTime leaveHour) {
            throw new NotImplementedException();
        }

        IEnumerable<ParkingSpot> IParkingSpotRepository.GetParkingPriceFreeSpotsAsync(decimal price) {
            throw new NotImplementedException();
        }
    }


}






