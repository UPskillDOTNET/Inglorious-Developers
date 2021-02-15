using CentralAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Services.IServices.TransactionTest
{
    public class Transaction : ITransaction
    {
        private readonly CentralAPIContext _context;

        public Transaction(CentralAPIContext context)
        {
            _context = context;
        }

        public void Begin()
        {
            _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _context.Database.CommitTransaction();
        }

        public void Rollback()
        {
            _context.Database.RollbackTransaction();
        }
    }
}
