using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpecializationService.Data;
using SpecializationService.Models;

namespace SpecializationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationsController : ControllerBase
    {
        private readonly SpecialDbContext _context;

        public SpecializationsController(SpecialDbContext context)
        {
            _context = context;
        }

        // GET: api/Specializations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Specialization>>> Getspecials()
        {
          if (_context.specials == null)
          {
              return NotFound();
          }
            return await _context.specials.ToListAsync();
        }

        // GET: api/Specializations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Specialization>> GetSpecialization(int id)
        {
          if (_context.specials == null)
          {
              return NotFound();
          }
            var specialization = await _context.specials.FindAsync(id);

            if (specialization == null)
            {
                return NotFound();
            }

            return specialization;
        }

        // PUT: api/Specializations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecialization(int id, Specialization specialization)
        {
            if (id != specialization.Id)
            {
                return BadRequest();
            }

            _context.Entry(specialization).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecializationExists(id))
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

        // POST: api/Specializations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Specialization>> PostSpecialization(Specialization specialization)
        {
          if (_context.specials == null)
          {
              return Problem("Entity set 'SpecialDbContext.specials'  is null.");
          }
            _context.specials.Add(specialization);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpecialization", new { id = specialization.Id }, specialization);
        }

        // DELETE: api/Specializations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialization(int id)
        {
            if (_context.specials == null)
            {
                return NotFound();
            }
            var specialization = await _context.specials.FindAsync(id);
            if (specialization == null)
            {
                return NotFound();
            }

            _context.specials.Remove(specialization);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpecializationExists(int id)
        {
            return (_context.specials?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
