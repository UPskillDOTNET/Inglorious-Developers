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
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(CentralAPIContext CentralAPIContext) : base(CentralAPIContext)
        {
        }

        public IEnumerable<Payment> GetPayments()
        {
            return GetAll().Include(p => p.User).ToList();
        }

        public IEnumerable<Payment> GetPaymentsByUserId(string userID)
        {
            return GetAll().Where(p => p.userID == userID).Include(p => p.User).ToList();
        }

        public async Task<Payment> SavePayment(Payment payment)
        {
            payment = await AddAsync(payment);
            return payment;
        }
    }
}
