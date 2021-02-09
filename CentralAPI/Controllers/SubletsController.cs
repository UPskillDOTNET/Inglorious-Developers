using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CentralAPI.Data;
using CentralAPI.Models;

namespace CentralAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubletsController : ControllerBase
    {
        private readonly CentralAPIContext _context;

        public SubletsController(CentralAPIContext context)
        {
            _context = context;
        }

        // GET: api/Sublets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sublet>>> GetSublets()
        {
            return await _context.Sublets.ToListAsync();
        }

        // GET: api/Sublets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sublet>> GetSublet(string id)
        {
            var sublet = await _context.Sublets.FindAsync(id);

            if (sublet == null)
            {
                return NotFound();
            }

            return sublet;
        }

        // PUT: api/Sublets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSublet(string id, Sublet sublet)
        {
            if (id != sublet.subletID)
            {
                return BadRequest();
            }

            _context.Entry(sublet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubletExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Sublets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sublet>> PostSublet(Sublet sublet)
        {
            _context.Sublets.Add(sublet);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SubletExists(sublet.subletID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSublet", new { id = sublet.subletID }, sublet);
        }

        // DELETE: api/Sublets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSublet(string id)
        {
            var sublet = await _context.Sublets.FindAsync(id);
            if (sublet == null)
            {
                return NotFound();
            }

            _context.Sublets.Remove(sublet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubletExists(string id)
        {
            return _context.Sublets.Any(e => e.subletID == id);
        }
    }
}
