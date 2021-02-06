﻿using CentralAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Repositories.IRepository
{
    public interface IWalletRepository
    {
        Task<IEnumerable<Wallet>> GetWallets();
    }
}
