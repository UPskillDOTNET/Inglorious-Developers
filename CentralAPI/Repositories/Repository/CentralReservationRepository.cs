using CentralAPI.Data;
using CentralAPI.Models;
using CentralAPI.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Repositories.Repository
{
    public class CentralReservationRepository : BaseRepository<CentralReservation>, ICentralReservationRepository
    {

        public CentralReservationRepository(CentralAPIContext centralAPIContext) : base(centralAPIContext)
        {
        }

        public async Task<IEnumerable<CentralReservation>> GetAllCentralReservations()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<IEnumerable<CentralReservation>> GetCentralReservationsNotCancelled()
        {
            return await GetAll().Where(r => r.isCancelled == false).Include(l => l.ParkingLot).Include(u => u.User).ToListAsync();
        }

        public async Task<IEnumerable<CentralReservation>> GetCentralReservationDateTimeNow()
        {
            return await GetAll().Where(r => r.startTime <= DateTime.Now && r.endTime >= DateTime.Now).Where(r => r.isCancelled == false).Where(r => r.forSublet == false).Include(l => l.ParkingLot).Include(u => u.User).ToListAsync();
        }

        public async Task<IEnumerable<CentralReservation>> GetSpecificCentralReservation(DateTime startDate, DateTime endDate)
        {
            return await GetAll().Where(r => (r.startTime >= startDate && r.endTime <= endDate) || (r.startTime <= endDate && r.endTime >= startDate)).Where(r => r.isCancelled == false).Where(r => r.forSublet == false).Include(l => l.ParkingLot).Include(u => u.User).ToListAsync();
        }

        public async Task<CentralReservation> GetCentralReservationById(string id)
        {
            return await GetAll().Include(p => p.ParkingLot).Include(u => u.User).FirstOrDefaultAsync(r => r.centralReservationID == id);
        }

        public async Task<CentralReservation> GetCentralReservationByUserId(string userID) {
            return await GetAll().Include(p => p.ParkingLot).Include(u => u.User).FirstOrDefaultAsync(r => r.userID == userID);
        }

        public async Task<bool> FindCentralReservationAny(string id)
        {
            return await GetAll().Where(p => p.centralReservationID == id).AnyAsync();
        }

        public async Task<bool> FindCentralReservationAnyByUser(string userID) {
            return await GetAll().Where(p => p.userID == userID).AnyAsync();
        }

        public async Task<bool> subletReservationAny(CentralReservation centralReservation)
        {
            return await GetAll().Where(r => ((r.startTime >= centralReservation.startTime && r.endTime <= centralReservation.endTime)
                                        || (r.startTime <= centralReservation.endTime && r.endTime >= centralReservation.startTime))
                                        && r.parkingLotID == centralReservation.parkingLotID && r.parkingSpotID == centralReservation.parkingSpotID && r.forSublet == true).AnyAsync();
        }
        public async Task<CentralReservation> GetsubletReservation(CentralReservation centralReservation)
        {
            return await GetAll().Where(r => ((r.startTime >= centralReservation.startTime && r.endTime <= centralReservation.endTime)
                                        || (r.startTime <= centralReservation.endTime && r.endTime >= centralReservation.startTime))
                                        && r.parkingLotID == centralReservation.parkingLotID && r.parkingSpotID == centralReservation.parkingSpotID && r.forSublet == true).FirstOrDefaultAsync();
        }
        public async Task<CentralReservation> PostCentralReservation(CentralReservation reservation)
        {
            await AddAsync(reservation);
            return reservation;
        }

        public async Task<CentralReservation> PatchCentralReservation(CentralReservation reservation)
        {
            reservation = await UpdateAsync(reservation);
            return reservation;
        }
    }
}


