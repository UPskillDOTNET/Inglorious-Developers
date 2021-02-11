using System;
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

namespace CentralAPI.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet]
        [Route("~/api/users/balance/{userID}")]
        public async Task<ActionResult<WalletDTO>> GetBalance(string userID)
        {
            if (_userService.GetUserById(userID) == null)
            {
                return BadRequest("User not found");
            }
            return await _walletService.GetBalance(userID);
        }

        [HttpGet]
        [Route("/api/[controller]/{walletID}")]
        public async Task<ActionResult<WalletDTO>> GetWalletById(string walletID)
        {
            return await _walletService.GetWalletById(walletID);
        }

        [HttpPost]
        public async Task<ActionResult<WalletDTO>> CreateWallet(string userID, string currency)
        {
            var resp = await _walletService.CreateWallet(userID, currency);
            var walletDTO = resp.Value;

            if (walletDTO == null)
            {
                return BadRequest();
            }
            return Ok(walletDTO);
        }
        [HttpPut]
        [Route("/api/[controller]/deposit/{walletID}/{value}/")]
        public async Task<ActionResult<WalletDTO>> DepositToWallet(string walletID, decimal value)
        {

            if (value == 0)
            {
                return BadRequest("Value is zero.");
            }

            await _walletService.DepositToWallet(walletID, value);
            return Ok(value + " euros added to the account sucessfully.");
        }

        [HttpPut]
        [Route("/api/[controller]/withdraw/{walletID}/{value}/")]
        public async Task<ActionResult<WalletDTO>> WithdrawFromWallet(string walletID, decimal value)
        {

            if (value == 0)
            {
                return BadRequest("Value is zero.");
            }

            await _walletService.WithdrawFromWallet(walletID, value);
            return Ok(value + " euros withdrawn from the account sucessfully.");
        }
    }
}




          