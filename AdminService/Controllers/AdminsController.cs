using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdminService.Data;
using AdminService.Models;

namespace AdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly AdministratorsDbContext _context;

        public AdminsController(AdministratorsDbContext context)
        {
            _context = context;
        }

        // GET: api/Admins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> Getadmins()
        {
            if (_context.administrators == null)
            {
                return NotFound();
            }
            return await _context.administrators.ToListAsync();
        }

        // GET: api/Admins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            if (_context.administrators == null)
            {
                return NotFound();
            }
            var admin = await _context.administrators.FindAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }

        // PUT: api/Admin/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmin(int id, Admin admin)
        {
            if (id != admin.AdminId)
            {
                return BadRequest();
            }

            _context.Entry(admin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(id))
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

        // POST: api/Admin
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Admin>> PostAdmin(Admin admin)
        {
            if (_context.administrators == null)
            {
                return Problem("Entity set 'SpecialDbContext.specials'  is null.");
            }
            _context.administrators.Add(admin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpecialization", new { id = admin.AdminId }, admin);
        }

        // DELETE: api/Admin/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            if (_context.administrators == null)
            {
                return NotFound();
            }
            var admin = await _context.administrators.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            _context.administrators.Remove(admin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminExists(int id)
        {
            return (_context.administrators?.Any(e => e.AdminId == id)).GetValueOrDefault();
        }
    }
}
