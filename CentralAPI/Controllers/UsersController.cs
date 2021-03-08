using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CentralAPI.Services.IServices;
using CentralAPI.DTO;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using CentralAPI.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;

namespace CentralAPI.Controllers
{
    // CONTROLLER: Users Controller

    [Route("central/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService, UserManager<User> userManager, IConfiguration configuration)
        {
            _userService = userService;
        }

        // HTTP GET: Get All Users
        // Gets all Users registered in the central API.

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            try
            {
                return await _userService.GetAllUsers();
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        // HTTP GET: Get User By ID
        // Gets a User registered in the central API, with a User ID provided in the route endpoint.

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(string id)
        {
            try
            {
                return await _userService.GetUserById(id);
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Register([FromBody]UserDTO userDTO)
        {
            var user = await _userService.CreateUser(userDTO);
           
            return Ok(user.Value);
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
