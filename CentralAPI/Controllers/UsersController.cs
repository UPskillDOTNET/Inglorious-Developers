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

        //// POST: api/Users
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<User>> PostUser(User user)
        //{
        //    _context.Users.Add(user);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (UserExists(user.userID))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetUser", new { id = user.userID }, user);
        //}

        //// DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser(string id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool UserExists(string id)
        //{
        //    return _context.Users.Any(e => e.userID == id);
        //}

        private async Task<bool> UserExists(string id)
        {
            var user = await _userService.GetUserById(id);

            if (user != null)
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
