using CentralAPI.Data;
using CentralAPI.DTO;
using CentralAPI.Models;
using CentralAPI.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Repositories.Repository
{
    public class ReservationPaymentRepository : BaseRepository<ReservationPayment>, IReservationPaymentRepository
    {
        public ReservationPaymentRepository(CentralAPIContext CentralAPIContext) : base(CentralAPIContext)
        {
        }

        public IEnumerable<ReservationPayment> GetReservationPayments()
        {
            return GetAll().Include(p => p.User).ToList();
        }

        public IEnumerable<ReservationPayment> GetReservationPaymentsByUserId(string userID)
        {
            return GetAll().Where(p => p.userID == userID).Include(p => p.User).ToList();
        }
        public async Task<ReservationPayment> SaveReservationPayment(ReservationPayment reservationPayment)
        {
            reservationPayment = await AddAsync(reservationPayment);
            return reservationPayment;
        }
    }
}
