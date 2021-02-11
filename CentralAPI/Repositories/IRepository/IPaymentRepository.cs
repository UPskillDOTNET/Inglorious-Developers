using CentralAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Repositories.IRepository
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        IEnumerable<Payment> GetPayments ();
        Payment GetPaymentsByUserId(string userID);
        Task<Payment> SavePayment(Payment payment);
    }
}
