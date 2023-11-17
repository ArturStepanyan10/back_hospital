using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdmissionService.Data;
using AdmissionService.Models;

namespace AdmissionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionsController : ControllerBase
    {
        private readonly AdmissionDbContext _context;

        public AdmissionsController(AdmissionDbContext context)
        {
            _context = context;
        }

        // GET: api/Admissions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admission>>> Getadmissions()
        {
          if (_context.admissions == null)
          {
              return NotFound();
          }
            return await _context.admissions.ToListAsync();
        }

        // GET: api/Admissions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admission>> GetAdmission(int id)
        {
          if (_context.admissions == null)
          {
              return NotFound();
          }
            var admission = await _context.admissions.FindAsync(id);

            if (admission == null)
            {
                return NotFound();
            }

            return admission;
        }

        // PUT: api/Admissions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmission(int id, Admission admission)
        {
            if (id != admission.AdmissionId)
            {
                return BadRequest();
            }

            _context.Entry(admission).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdmissionExists(id))
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

        // POST: api/Admissions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Admission>> PostAdmission(Admission admission)
        {
          if (_context.admissions == null)
          {
              return Problem("Entity set 'AdmissionDbContext.admissions'  is null.");
          }
            _context.admissions.Add(admission);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdmission", new { id = admission.AdmissionId }, admission);
        }

        // DELETE: api/Admissions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmission(int id)
        {
            if (_context.admissions == null)
            {
                return NotFound();
            }
            var admission = await _context.admissions.FindAsync(id);
            if (admission == null)
            {
                return NotFound();
            }

            _context.admissions.Remove(admission);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdmissionExists(int id)
        {
            return (_context.admissions?.Any(e => e.AdmissionId == id)).GetValueOrDefault();
        }
    }
}
