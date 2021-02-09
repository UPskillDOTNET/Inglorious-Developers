using CentralAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Services.IServices
{
    public interface ITransactionService
    {

        Task<ActionResult<IEnumerable<TransactionDTO>>> GetAllTransactions();
        Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactionsByUserID(string userID);
        Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactionsByUserAndDate(string userID);
        Task<ActionResult<IEnumerable<TransactionDTO>>> GetOperationTransactionsByUserID(string userID, string operation);

    }
}
