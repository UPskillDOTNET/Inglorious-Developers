﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CentralAPI.Data;
using CentralAPI.Models;
using CentralAPI.Services.IServices;
using CentralAPI.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace CentralAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("central/[controller]")]
    [ApiController]
    public class WalletsController : Controller
    {
        private readonly IWalletService _walletService;
        private readonly IUserService _userService;

        public WalletsController(IWalletService walletService, IUserService userService)
        {
            _walletService = walletService;
            _userService = userService;
        }

        // GET: Wallets
        [HttpGet]
        public Task<ActionResult<IEnumerable<WalletDTO>>> GetAllWallets()
        {
            return _walletService.GetAllWallets();
        }

        [HttpGet("balance/{userID}")]
        public async Task<ActionResult<WalletDTO>> GetBalance(string userID)
        {
            if (await _userService.GetUserById(userID) == null)
            {
                return BadRequest("User not found");
            }

            if (!await WalletExists(userID))
            {
                return NotFound("Wallet not found");
            }
            return await _walletService.GetBalance(userID);
        }

        [HttpGet("{WalletID}")]
        public async Task<ActionResult<WalletDTO>> GetWalletById(string walletID)
        {
            var wallet = await _walletService.GetWalletById(walletID);
            if (wallet.Value == null) {
                return NotFound();
            }
            return await _walletService.GetWalletById(walletID);
        }

        [HttpPost]
        public async Task<ActionResult<WalletDTO>> CreateWallet(string userID)
        {
            var resp = await _walletService.CreateWallet(userID);
            var walletDTO = resp.Value;

            if (walletDTO == null)
            {
                return BadRequest();
            }
            return Ok(walletDTO);
        }
        [HttpPut]
        [Route("deposit/{walletID}/{value}/")]
        public async Task<ActionResult<WalletDTOOperation>> DepositToWallet(string walletID, decimal value)
        {

            if (value == 0)
            {
                return BadRequest("Value is zero.");
            }
            if (value < 0) {
                return BadRequest("Value is negative.");
            }
           var result = await _walletService.DepositToWallet(walletID, value);
            return Ok(result);
        }

        [HttpPut]
        [Route("/api/[controller]/withdraw/{walletID}/{value}/")]
        public async Task<ActionResult<WalletDTOOperation>> WithdrawFromWallet(string walletID, decimal value)
        {

            if (value == 0)
            {
                return BadRequest("Value is zero.");
            }
            if (value < 0) {
                return BadRequest("Value is negative.");
            }
            var result = await _walletService.WithdrawFromWallet(walletID, value);
            return Ok(result);
        }

        private async Task<bool> WalletExists(string id)
        {
            var wallet = await _walletService.GetWalletById(id);

            if (wallet.Value != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}




          