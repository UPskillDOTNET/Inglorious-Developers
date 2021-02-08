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

        // POST: Wallets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult<WalletDTO>> CreateWallet(string userID, string currency)
        {
            userID = "batata";
            currency = "test";
            var resp = await _walletService.CreateWallet(userID, currency);
            var walletDTO = resp.Value;

            if (walletDTO == null)
            {
                return BadRequest();
            }
            return Ok(walletDTO);
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult<WalletDTO>> UpdateWallet(string id, WalletDTO walletDTO, decimal value)
        //{
        //    var Results = _walletService.Validate(walletDTO);

        //    if (!Results.IsValid)
        //    {
        //        return BadRequest("Can't update " + Results);
        //    }

        //    if (id != walletDTO.walletID)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}


        //[HttpPut("{id}")]
        //public async Task<ActionResult<ParkingSpotDTO>> PutParkingSpot(string id, ParkingSpotDTO parkingSpotDTO)
        //{
        //    var Results = _parkingSpotService.Validate(parkingSpotDTO);

        //    if (!Results.IsValid)
        //    {
        //        return BadRequest("Can't update " + Results);
        //    }

        //    if (id != parkingSpotDTO.parkingSpotID)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        await _parkingSpotService.PutParkingSpot(id, parkingSpotDTO);
        //    }
        //    catch (Exception)
        //    {
        //        if (await ParkingSpotExists(id) == false)
        //        {
        //            return NotFound("The Parking Spot you were trying to update could not be found");
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return NoContent();
        //}

        private async Task<bool> WalletExists(string id)
        {
            return await _walletService.FindWalletAny(id);
        }
    }
}




          