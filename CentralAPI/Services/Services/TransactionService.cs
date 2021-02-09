using AutoMapper;
using CentralAPI.DTO;
using CentralAPI.Models;
using CentralAPI.Repositories.IRepository;
using CentralAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Services.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactions()
        {
            var transactions =  await _transactionRepository.GetTransactions();
            var transactionsDTO = _mapper.Map<List<Transaction>, List<TransactionDTO>>(transactions.ToList());
            return transactionsDTO;
        }

        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactionsByUserID(string userID)
        {
            var transactions = await _transactionRepository.GetTransactionsByUserID(userID);
            var transactionsDTO = _mapper.Map<List<Transaction>, List<TransactionDTO>>(transactions.ToList());
            return transactionsDTO;
        }

        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetOperationTransactionsByUserID(string userID, string operation)
        {
            var transactions = await _transactionRepository.GetOperationTransactionsByUser(userID, operation);
            var transactionsDTO = _mapper.Map<List<Transaction>, List<TransactionDTO>>(transactions.ToList());
            return transactionsDTO;
        }

        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactionsByUserAndDate(string userID, DateTime dateTime)
        {
            var transactions = await _transactionRepository.GetTransactionsByUserAndDate(userID, dateTime);
            var transactionsDTO = _mapper.Map<List<Transaction>, List<TransactionDTO>>(transactions.ToList());
            return transactionsDTO;
        }

        public async Task<ActionResult<TransactionDTO>> CreateTransaction(string userID, string operation, decimal value)
        {
            Transaction transaction = new Transaction
            {
                operation = operation,
                userID = userID,
                value = value,
                transactionDate = DateTime.Now
            };

            await _transactionRepository.CreateTransaction(transaction);
            var transactionDTO = _mapper.Map<Transaction, TransactionDTO>(transaction);
            return transactionDTO;
        }
    }
}
