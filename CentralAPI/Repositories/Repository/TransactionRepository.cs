﻿using CentralAPI.Data;
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

        public IEnumerable<Transaction> GetOperationTransactionsByUserAndDate()
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Transaction> GetTransactionsByUserAndDate()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetTransactionsByUserID()
        {
            throw new NotImplementedException();
        }
    }
}
