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

        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            return await GetAll().Include(w => w.User).ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByUserID(string userID)
        {
            return await GetAll().Where(t => t.userID == userID).Include(w => w.User).ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetOperationTransactionsByUser(string userID, string operation)
        {
            return await GetAll().Where(t => t.userID == userID && t.operation == operation).Include(w => w.User).ToListAsync();
        }


        public async Task<IEnumerable<Transaction>> GetTransactionsByUserAndDate(string userID, DateTime dateTime)
        {
            return await GetAll().Where(t => t.userID == userID && t.transactionDate == dateTime).Include(w => w.User).ToListAsync();
        }

        public async Task<Transaction> CreateTransaction(Transaction transaction)
        {
            await AddAsync(transaction);
            return transaction;
        }
    }
}
