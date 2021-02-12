using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CentralAPI.Services.IServices;
using CentralAPI.DTO;
using CentralAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CentralAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IWalletService _walletService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Users
        [HttpGet]
        public Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(string id)
        {

            if (await UserExists(id) == false)
            {
                return NotFound("User not Found");
            }
            return await _userService.GetUserById(id);
        }

        // O Nif não pode ser alterado, quando não se escreve o nif no body deste método, o nif fica a null
        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDTO>> UpdateUserById(string id, [FromBody] UserDTO userDTO)
        {
            try
            {
                await _userService.UpdateUserById(id, userDTO);
            }
            catch (Exception)
            {
                if (await UserExists(id) == false)
                {
                    return NotFound("User not found.");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        [Route("/api/[controller]/{currency}/")]
        public async Task<ActionResult<UserDTO>> CreateUser (UserDTO userDTO, string currency)
        {
            var userDto = await _userService.CreateUser(userDTO);
            

            if(userDto.Value == null)
            {
                return BadRequest();
            }

            await _walletService.CreateWallet(userDto.Value.userID, currency);

            return Ok(userDTO);
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDTO>> DeleteUserProfile(string id)
        {

            if (await UserExists(id) == false)
            {
                return NotFound("User does not exist.");
            }
            else
            {
                await _userService.DeleteUserProfile(id);
            }
            return Ok();
        }

        //private bool UserExists(string id)
        //{
        //    return _context.Users.Any(e => e.userID == id);
        //}

        private async Task<bool> UserExists(string id)
        {
            var user = await _userService.GetUserById(id);

            if (user.Value != null)
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
