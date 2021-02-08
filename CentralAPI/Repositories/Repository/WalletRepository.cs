﻿using CentralAPI.Data;
using CentralAPI.Models;
using CentralAPI.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Repositories.Repository
{
    public class WalletRepository : BaseRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(CentralAPIContext CentralAPIContext) : base(CentralAPIContext)
        {
        }

        public IEnumerable<Wallet> GetWallets()
        {
            return GetAll().Include(w => w.User).ToList();
        }

        public Wallet GetBalance(string userID)
        {
            return GetAll().Where(w => w.userID == userID).FirstOrDefault();
        }

        public async Task<Wallet> CreateWallet(Wallet wallet)
        {
            await AddAsync(wallet);
            return wallet;
        }

        public Wallet UpdateBalance()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> FindWalletAny(string id)
        {
            return await GetAll().Where(p => p.walletID == id).AnyAsync();
        }
    }
}
