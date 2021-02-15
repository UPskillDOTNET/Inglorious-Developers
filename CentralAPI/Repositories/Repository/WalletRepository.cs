using CentralAPI.Data;
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

        public async Task<IEnumerable<Wallet>> GetWallets()
        {
            return  await GetAll().Include(w => w.User).ToListAsync();
        }

        public async Task<Wallet> GetBalance(string userID)
        {
            return await GetAll().Where(w => w.userID == userID).Include(w => w.User).FirstOrDefaultAsync();
        }

        public async Task<Wallet> GetWalletById(string walletID)
        {
            return await GetAll().Where(w => w.walletID == walletID).Include(w => w.User).FirstOrDefaultAsync();
        }

        public async Task<Wallet> CreateWallet(Wallet wallet)
        {
            await AddAsync(wallet);
            return wallet;
        }

        public async Task<Wallet> DepositToWallet(Wallet wallet, decimal value)
        {
            await UpdateAsync(wallet);
            return wallet;
        }

        public async Task<Wallet> WithdrawFromWallet(Wallet wallet, decimal value)
        {
            await UpdateAsync(wallet);
            return wallet;
        }

        public async Task<bool> FindWalletAny(string id)
        {
            return await GetAll().Where(p => p.walletID == id).AnyAsync();
        }
    }
}
