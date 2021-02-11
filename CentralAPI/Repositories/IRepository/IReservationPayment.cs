using CentralAPI.DTO;
using CentralAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Repositories.IRepository
{
    public interface IReservationPaymentRepository : IBaseRepository<ReservationPayment>
    {
       IEnumerable<ReservationPayment> GetReservationPayments();
       IEnumerable<ReservationPayment> GetReservationPaymentsByUserId(string userID);
       Task<ReservationPayment> SaveReservationPayment(ReservationPayment reservationPayment);
    }
}
