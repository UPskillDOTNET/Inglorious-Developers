using CentralAPI.Data;
using CentralAPI.DTO;
using CentralAPI.Models;
using CentralAPI.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Repositories.Repository
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(CentralAPIContext CentralAPIContext) : base(CentralAPIContext)
        {
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            return GetAll().Include(w => w.User).ToList();
        }

        public IEnumerable<Transaction> GetTransactionsByUserID(string userID)
        {
            return GetAll().Where(t => t.userID == userID).Include(w => w.User).ToList();
        }

        public IEnumerable<Transaction> GetOperationTransactionsByUser(string userID, string operation)
        {
            return GetAll().Where(t => t.userID == userID && t.operation == operation).Include(w => w.User).ToList();
        }


        public IEnumerable<Transaction> GetTransactionsByUserAndDate()
        {
            throw new NotImplementedException();
        }
    }
}
