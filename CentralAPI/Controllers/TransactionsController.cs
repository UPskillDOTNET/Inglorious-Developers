using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CentralAPI.Data;
using CentralAPI.Models;
using CentralAPI.Services.IServices;

namespace CentralAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactions()
        {
            return _transactionService.GetTransactions();
        }

        [HttpGet]
        [Route("~/api/users/transactions/{userID}")]
        public Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactionsByUserID(string userID)
        {
            return _transactionService.GetTransactionsByUserID(userID);
        }

        [HttpGet]
        [Route("~/api/users/transactions/{userID}/{operation}")]
        public Task<ActionResult<IEnumerable<TransactionDTO>>> GetOperationTransactionsByUserID(string userID, string operation)
        {
            return _transactionService.GetOperationTransactionsByUserID(userID, operation);
        }



        //// GET: Transactions/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var transaction = await _context.Transaction
        //        .FirstOrDefaultAsync(m => m.transactionID == id);
        //    if (transaction == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(transaction);
        //}

        //// POST: Transactions/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var transaction = await _context.Transaction.FindAsync(id);
        //    _context.Transaction.Remove(transaction);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool TransactionExists(string id)
        //{
        //    return _context.Transaction.Any(e => e.transactionID == id);
        //}
    }
}
