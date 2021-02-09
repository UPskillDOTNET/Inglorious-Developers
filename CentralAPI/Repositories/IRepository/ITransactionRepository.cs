using CentralAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Repositories.IRepository
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        IEnumerable<Transaction> GetTransactions();
        IEnumerable<Transaction> GetTransactionsByUserID();
        IEnumerable<Transaction> GetTransactionsByUserAndDate();
        IEnumerable<Transaction> GetOperationTransactionsByUserAndDate();
    }
}
