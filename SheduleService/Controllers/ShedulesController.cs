using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SheduleService.Data;
using SheduleService.Models;

namespace SheduleService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShedulesController : ControllerBase
    {
        private readonly SheduleDbContext _context;

        public ShedulesController(SheduleDbContext context)
        {
            _context = context;
        }

        // GET: api/Shedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shedule>>> Getshedules()
        {
          if (_context.shedules == null)
          {
              return NotFound();
          }
            return await _context.shedules.ToListAsync();
        }

        // GET: api/Shedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shedule>> GetShedule(int id)
        {
          if (_context.shedules == null)
          {
              return NotFound();
          }
            var shedule = await _context.shedules.FindAsync(id);

            if (shedule == null)
            {
                return NotFound();
            }

            return shedule;
        }

        // PUT: api/Shedules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShedule(int id, Shedule shedule)
        {
            if (id != shedule.SheduleId)
            {
                return BadRequest();
            }

            _context.Entry(shedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SheduleExists(id))
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

        // POST: api/Shedules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shedule>> PostShedule(Shedule shedule)
        {
          if (_context.shedules == null)
          {
              return Problem("Entity set 'SheduleDbContext.shedules'  is null.");
          }
            _context.shedules.Add(shedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShedule", new { id = shedule.SheduleId }, shedule);
        }

        // DELETE: api/Shedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShedule(int id)
        {
            if (_context.shedules == null)
            {
                return NotFound();
            }
            var shedule = await _context.shedules.FindAsync(id);
            if (shedule == null)
            {
                return NotFound();
            }

            _context.shedules.Remove(shedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SheduleExists(int id)
        {
            return (_context.shedules?.Any(e => e.SheduleId == id)).GetValueOrDefault();
        }
    }
}
