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
        Wallet GetWalletById(string walletID);
        Task<Wallet> CreateWallet(Wallet wallet);
        Task<Wallet> DepositToWallet(Wallet wallet, decimal value);
        Task<Wallet> WithdrawFromWallet(Wallet wallet, decimal value);
        Task<bool> FindWalletAny(string id);
    }
}
