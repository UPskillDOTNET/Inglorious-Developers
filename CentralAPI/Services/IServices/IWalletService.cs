using CentralAPI.DTO;
using CentralAPI.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Services.IServices
{
    public interface IWalletService
    {
        Task<ActionResult<IEnumerable<WalletDTO>>> GetAllWallets();
        Task<ActionResult<WalletDTO>> GetBalance(string userID);
        Task<ActionResult<WalletDTO>> CreateWallet(string userID, string currency);
        Task<bool> FindWalletAny(string id);
        ValidationResult Validate(WalletDTO walletDTO);
    }
}
