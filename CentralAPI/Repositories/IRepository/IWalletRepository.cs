using CentralAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Repositories.IRepository
{
    public interface IWalletRepository : IBaseRepository<Wallet>
    {
        IEnumerable<Wallet> GetWallets();
        Wallet GetBalance(string userID);
        Task<Wallet> CreateWallet(Wallet wallet);
        Task<Wallet> DepositToWallet(string walletID, decimal value);
        Task<Wallet> WithdrawFromWallet(string walletID, decimal value);
        Task<bool> FindWalletAny(string id);
    }
}
