using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CentralAPI.Repositories;

using CentralAPI.Models;
using CentralAPI.Repositories.Repository;

namespace CentralAPI.Repositories.Repository {
    //public class CentralReservationRepository : BaseRepository<CentralReservation>, ICentralReservationRepository {

            //public CentralReservationRepository(CentralAPIContext centralAPIContext) : base(centralAPIContext) {
            //}

            //public async Task<IEnumerable<CentralReservation>> GetCentralReservations() {
            //    return await GetAll().ToListAsync();
            //}

            //public async Task<IEnumerable<CentralReservation>> GetCentralReservationsNotCancelled() {
            //    return await GetAll().Where(r => r.isCancelled == false).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
            //}

            //public async Task<IEnumerable<CentralReservation>> GetCentralReservationDateTimeNow() {
            //    return await GetAll().Where(r => r.startTime <= DateTime.Now && r.endTime >= DateTime.Now).Where(r => r.isCancelled == true).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
            //}

            //public async Task<IEnumerable<CentralReservation>> GetSpecificCentralReservation(DateTime startDate, DateTime endDate) {
            //    return await GetAll().Where(r => (r.startTime >= startDate && r.endTime <= endDate) || (r.startTime <= endDate && r.endTime >= startDate)).Where(r => r.isCancelled == true).Include(s => s.ParkingSpot).ThenInclude(s => s.ParkingLot).ToListAsync();
            //}

            //public async Task<CentralReservation> GetCentralReservation(string id) {
            //    return await GetAll().Include(s => s.ParkingSpot).ThenInclude(p => p.ParkingLot).FirstOrDefaultAsync(r => r.reservationID == id);
            //}
            //public async Task<bool> FindCentralReservationAny(string id) {
            //    return await GetAll().Where(p => p.reservationID == id).AnyAsync();
            //}
            //public async Task<CentralReservation> PostCentralReservation(CentralReservation reservation) {
            //    await AddAsync(reservation);
            //    return reservation;
            //}

            //public async Task<CentralReservation> PatchReservation(string id) {
            //    var x = GetAll().Include(s => s.ParkingSpot).ThenInclude(p => p.ParkingLot)
            //        .FirstOrDefaultAsync(r => r.reservationID == id)
            //        .Result;
            //    x.isCancelled = true;
            //    await UpdateAsync(x);
            //    return x;
            //}

        
    //}
}


