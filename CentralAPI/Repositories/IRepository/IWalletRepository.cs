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
        Wallet CreateWallet();
        Wallet GetBalance(string userID);
        Wallet UpdateBalance();
    }
}
