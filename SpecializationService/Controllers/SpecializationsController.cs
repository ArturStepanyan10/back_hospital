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

        // GET: api/Specializations ONLY ADMIN
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Specialization>>> Getspecials()
        {
          if (_context.specials == null)
          {
              return NotFound("Записей не найдено");
          }
            return await _context.specials.ToListAsync();
        }

        // GET: api/Specializations/5 ONLY ADMIN
        [HttpGet("{id}")]
        public async Task<ActionResult<Specialization>> GetSpecialization(int id)
        {

            if (id <= 0)
            {
                return BadRequest("Неверный идентификатор!");
            }

            try
            {
                if (_context.specials == null)
                {
                    return NotFound("Записей не найдено");
                }
                var specialization = await _context.specials.FindAsync(id);

                if (specialization == null)
                {
                    return NotFound($"Запись специализации с ID {id} не найдена");
                }

                return specialization;
            }
            catch (Exception)
            {
                // Логирование ошибки или другие действия по обработке ошибки
                return StatusCode(500, "Internal Server Error");
            }
        }

        // PUT: api/Specializations/5 ONLY ADMIN
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecialization(int id, Specialization specialization)
        {
            if (id != specialization.SpecializationId)
            {
                return BadRequest($"Данное ID {id} не найдено!");
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
                    return NotFound($"Запись специализации с ID {id} не найдена");
                }
                else
                {
                    return StatusCode(409, "Произошел конфликт. Пожалуйста, обновите данные и повторите попытку.");
                }
            }

            return Ok("Запись успешно изменена!");
        }

        // POST: api/Specializations ONLY ADMIN
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

            return CreatedAtAction("GetSpecialization", new { id = specialization.SpecializationId }, specialization);
        }

        // DELETE: api/Specializations/5 ONLY ADMIN
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialization(int id)
        {
            if (_context.specials == null)
            {
                return NotFound("Записей не найдено");
            }
            var specialization = await _context.specials.FindAsync(id);
            if (specialization == null)
            {
                return NotFound($"Запись специализации с ID {id} не найдена");
            }

            _context.specials.Remove(specialization);
            await _context.SaveChangesAsync();

            return Ok("Запись успешно удалена!");
        }

        private bool SpecializationExists(int id)
        {
            return (_context.specials?.Any(e => e.SpecializationId == id)).GetValueOrDefault();
        }
    }
}
