using AutoMapper;
using CentralAPI.DTO;
using CentralAPI.Models;
using CentralAPI.Repositories.IRepository;
using CentralAPI.Services.IServices;
using CentralAPI.Utils;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Services.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IMapper _mapper;
        //private readonly IWalletPaymentService _paymentService;

        public WalletService(IWalletRepository walletRepository, IMapper mapper)
        {
            _walletRepository = walletRepository;
            _mapper = mapper;
        }


        public async Task<ActionResult<IEnumerable<WalletDTO>>> GetAllWallets()
        {
          
            var wallets = _walletRepository.GetWallets();
            var walletsDTO = _mapper.Map<List<Wallet>, List<WalletDTO>>(wallets.ToList());
            return walletsDTO;
        }

        public async Task<ActionResult<WalletDTO>> GetBalance(string userID)
        {
         
            var wallet = _walletRepository.GetBalance(userID);
            var walletDTO = _mapper.Map<Wallet, WalletDTO>(wallet);
            return walletDTO;
        }

        public async Task<ActionResult<WalletDTO>> GetWalletById(string walletID)
        {
            var wallet = _walletRepository.GetWalletById(walletID);
            var walletDTO = _mapper.Map<Wallet, WalletDTO>(wallet);
            return walletDTO;
        }
        
        public async Task<ActionResult<WalletDTO>> CreateWallet(string userID, string currency)
        {
            if (currency == null)
            {
                currency = "euro";
            }

            Wallet wallet = new Wallet {
                walletID = userID,
                totalAmount = 0,
                currency = currency,
                userID = userID
            };
            await _walletRepository.CreateWallet(wallet);
            var walletDTO = _mapper.Map<Wallet, WalletDTO>(wallet);
            return walletDTO;
        }

        public async Task<ActionResult<WalletDTOOperation>> DepositToWallet(string walletID, decimal value)
        {
            var wallet = _walletRepository.GetWalletById(walletID);
            WalletDTOOperation walletDTOOperation;

            if (!wallet.deposit(value))
            {
                walletDTOOperation = _mapper.Map<Wallet, WalletDTOOperation>(wallet);
                walletDTOOperation.message = "Operation not allowed. Can't charge negative numbers.";
                walletDTOOperation.isSuccess = false;
                walletDTOOperation.operation = 1;
            } else
            {
                walletDTOOperation = _mapper.Map<Wallet, WalletDTOOperation>(wallet);
                walletDTOOperation.message = "Operation not allowed. Can't charge negative numbers.";
                walletDTOOperation.isSuccess = true;
                walletDTOOperation.operation = 1;
                await _walletRepository.DepositToWallet(wallet, value);
            }
            return walletDTOOperation;
        }

        public async Task<ActionResult<WalletDTOOperation>> WithdrawFromWallet(string walletID, decimal value)
        {

            var wallet = _walletRepository.GetWalletById(walletID);
            WalletDTOOperation walletDTOOperation;

           if (!wallet.withdraw(value))
            {
                walletDTOOperation = _mapper.Map<Wallet,WalletDTOOperation>(wallet);
                walletDTOOperation.message = "Operation not allowed. Insufficient funds.";
                walletDTOOperation.isSuccess = false;
                walletDTOOperation.operation = 2;
            } else
            {
                walletDTOOperation = _mapper.Map<Wallet, WalletDTOOperation>(wallet);
                walletDTOOperation.message = "Operation allowed.";
                walletDTOOperation.isSuccess = true;
                walletDTOOperation.operation = 2;
                await _walletRepository.WithdrawFromWallet(wallet, value);
            }

            return walletDTOOperation;
        }

        public async Task<bool> FindWalletAny(string id)
        {
            return await _walletRepository.FindWalletAny(id);
        }

        public ValidationResult Validate(WalletDTO walletDTO)
        {
            WalletValidator validationRules = new WalletValidator();
            ValidationResult Results = validationRules.Validate(walletDTO);
            return Results;
        }
    }
}
