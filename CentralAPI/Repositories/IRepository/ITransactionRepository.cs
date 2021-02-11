using CentralAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Repositories.IRepository
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetTransactions();
        Task<IEnumerable<Transaction>> GetTransactionsByUserID(string userID);
        Task<IEnumerable<Transaction>> GetOperationTransactionsByUser(string userID, string operation);
        Task<IEnumerable<Transaction>> GetTransactionsByUserAndDate(string userID, DateTime dateTime);
        Task<Transaction> CreateTransaction(Transaction transaction);
    }
}
