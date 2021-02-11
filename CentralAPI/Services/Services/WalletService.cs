﻿using AutoMapper;
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
        private readonly IPaymentService _paymentService;

        public WalletService(IWalletRepository walletRepository, IMapper mapper)
        {
            _walletRepository = walletRepository;
            _mapper = mapper;
        }


        public async Task<ActionResult<IEnumerable<WalletDTO>>> GetAllWallets()
        {
            //return await new Task<ActionResult<IEnumerable<WalletDTO>>>(() =>
            //{
            //    var wallets = _walletRepository.GetWallets();
            //    var walletsDTO = _mapper.Map<List<Wallet>, List<WalletDTO>>(wallets.ToList());
            //    return walletsDTO;
            //});
            var wallets = _walletRepository.GetWallets();
            var walletsDTO = _mapper.Map<List<Wallet>, List<WalletDTO>>(wallets.ToList());
            return walletsDTO;
        }

        public async Task<ActionResult<WalletDTO>> GetBalance(string userID)
        {
            //return await new Task<ActionResult<WalletDTO>>(() =>
            //{
            //    var wallet = _walletRepository.GetBalance(userID);
            //    var walletDTO = _mapper.Map<Wallet, WalletDTO>(wallet);
            //    return walletDTO;
            //});
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

        public async Task<ActionResult<Wallet>> DepositToWallet(string walletID, decimal value)
        {
  
            return await _walletRepository.DepositToWallet(walletID, value);

            // Alterar com o valor que recebe no argumento - nao pode ser menor que 0 ou a operação é cancelada
        }

        public async Task<ActionResult<Wallet>> WithdrawFromWallet(string walletID, decimal value)
        {
            return await _walletRepository.WithdrawFromWallet(walletID, value);
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