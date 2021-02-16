using CentralAPI.DTO;
using CentralAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentralAPI.Controllers
{
    [Authorize]
    [Route("central/[controller]")]
    [ApiController]
    public class SubletsController : ControllerBase
    {
        private readonly ISubletService _subletService;

        public SubletsController(ISubletService subletService)
        {
            _subletService = subletService;
        }

        // GET: api/Sublets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubletDTO>>> GetSublets()
        {
            return await _subletService.GetSublets();
        }

        // GET: api/Sublets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubletDTO>> GetSublet(string id)
        {
            var sublet = await _subletService.GetSublet(id);

            if (sublet == null)
            {
                return NotFound();
            }

            return sublet;
        }


        // POST: api/Sublets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubletDTO>> PostSublet([FromBody] SubletDTO subletDTO)
        {

            try
            {
                await _subletService.CreateSublet(subletDTO);
            }
            catch (DbUpdateException)
            {
                if (SubletExists(subletDTO.subletID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSublet", new { id = subletDTO.subletID }, subletDTO);
        }

        // DELETE: api/Sublets/5
        [HttpPut("{id}")]
        public async Task<ActionResult<SubletDTO>> CancelSublet(string id)
        {

            try
            {

                return await _subletService.CancelSublet(id);
            }

            catch (Exception ex)
            {
                return BadRequest(ex);
            }


        }

        private bool SubletExists(string id)
        {
            if (_subletService.GetSublet(id) == null)
            {
                return false;
            }
            return true;
        }



    }
}
