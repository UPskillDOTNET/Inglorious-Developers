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

        Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactions();
        Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactionsByUserID(string userID);
        Task<ActionResult<IEnumerable<TransactionDTO>>> GetOperationTransactionsByUserID(string userID, string operation);
        Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactionsByUserAndDate(string userID, DateTime dateTime);
        Task<ActionResult<TransactionDTO>> CreateTransaction(string userID, string operation, decimal value);
    }
}
