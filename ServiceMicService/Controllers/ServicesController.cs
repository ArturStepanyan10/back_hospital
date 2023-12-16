using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceMicService.Data;
using ServiceMicService.Model;

namespace ServiceMicService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly ServiceDbContext _context;

        public ServicesController(ServiceDbContext context)
        {
            _context = context;
        }

        // GET: api/Services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> Getservices()
        {
            return await _context.services.ToListAsync();
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Неверный идентификатор!");
            }
            try
            {
                var service = await _context.services.FindAsync(id);

                if (service == null)
                {
                    return NotFound("Записей не найдено");
                }

                return service;
            }
            catch (Exception)
            {
                // Логирование ошибки или другие действия по обработке ошибки
                return StatusCode(500, "Internal Server Error");
            }
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, Service service)
        {
            if (id != service.Id)
            {
                return BadRequest($"Данное ID {id} не найдено!");
            }

            _context.Entry(service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
                {
                    return NotFound($"Запись услуги с ID {id} не найдена");
                }
                else
                {
                    return StatusCode(409, "Произошел конфликт. Пожалуйста, обновите данные и повторите попытку.");
                }
            }

            return Ok("Запись успешно изменена!");
        }

        // POST: api/Services
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Service>> PostService(Service service)
        {
            _context.services.Add(service);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetService", new { id = service.Id }, service);
        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.services.FindAsync(id);
            if (service == null)
            {
                return NotFound($"Запись услуги с ID {id} не найдена");
            }

            _context.services.Remove(service);
            await _context.SaveChangesAsync();

            return Ok("Запись успешно удалена!");
        }

        private bool ServiceExists(int id)
        {
            return _context.services.Any(e => e.Id == id);
        }
    }
}
