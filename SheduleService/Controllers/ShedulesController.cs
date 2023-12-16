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

        // GET: api/Shedules ONLY ADMIN AND DOCTOR
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shedule>>> Getshedules()
        {
          if (_context.shedules == null)
          {
              return NotFound("Записей не найдено");
          }
            return await _context.shedules.ToListAsync();
        }

        // GET: api/Shedules/5 ONLY ADMIN AND DOCTOR
        [HttpGet("{id}")]
        public async Task<ActionResult<Shedule>> GetShedule(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Неверный идентификатор!");
            }

            try
            {

                if (_context.shedules == null)
                {
                    return NotFound("Записей не найдено");
                }
                var shedule = await _context.shedules.FindAsync(id);

                if (shedule == null)
                {
                    return NotFound($"Запись расписания с ID {id} не найдена");
                }

                return shedule;
            }
            catch (Exception)
            {
                // Логирование ошибки или другие действия по обработке ошибки
                return StatusCode(500, "Internal Server Error");
            }
        }

        // PUT: api/Shedules/5 ONLY ADMIN
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShedule(int id, Shedule shedule)
        {
            if (id != shedule.Id)
            {
                return BadRequest($"Данное ID {id} не найдено!");
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
                    return NotFound($"Запись расписания с ID {id} не найдена");
                }
                else
                {
                    return StatusCode(409, "Произошел конфликт. Пожалуйста, обновите данные и повторите попытку.");
                }
            }

            return Ok("Запись успешно изменена!");
        }


        // POST: api/Shedules ONLY ADMIN
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

            return CreatedAtAction("GetShedule", new { id = shedule.Id }, shedule);
        }

        // DELETE: api/Shedules/5 ONLY ADMIN
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShedule(int id)
        {
            if (_context.shedules == null)
            {
                return NotFound("Записей не найдено");
            }
            var shedule = await _context.shedules.FindAsync(id);
            if (shedule == null)
            {
                return NotFound($"Запись расписания с ID {id} не найдена");
            }

            _context.shedules.Remove(shedule);
            await _context.SaveChangesAsync();

            return Ok("Запись успешно удалена!");
        }

        private bool SheduleExists(int id)
        {
            return (_context.shedules?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
