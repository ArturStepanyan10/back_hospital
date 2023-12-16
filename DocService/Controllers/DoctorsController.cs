using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DocService.Data;
using DocService.Models;



namespace DocService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorDbContext _context;

        public DoctorsController(DoctorDbContext context)
        {
            _context = context;
        }

        // GET: api/Doctors ONLY ADMIN
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> Getdoctors()
        {
          if (_context.doctors == null)
          {
              return NotFound("Записей не найдено");
          }
            return await _context.doctors.ToListAsync();
        }

        // GET: api/Doctors/5 ONLY ADMIN
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Неверный идентификатор!");
            }

            try
            {

                if (_context.doctors == null)
                {
                    return NotFound("Записей не найдено");
                }
                var doctor = await _context.doctors.FindAsync(id);

                if (doctor == null)
                {
                    return NotFound($"Запись доктора с ID {id} не найдена");
                }

                return doctor;
            }

            catch (Exception)
            {
                // Логирование ошибки или другие действия по обработке ошибки
                return StatusCode(500, "Internal Server Error");
            }
        }

        // PUT: api/Doctors/5 ONLY ADMIN
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(int id, Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return BadRequest($"Данное ID {id} не найдено!");
            }

            _context.Entry(doctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
                {
                    return NotFound($"Запись доктора с ID {id} не найдена");
                }
                else
                {
                    return StatusCode(409, "Произошел конфликт. Пожалуйста, обновите данные и повторите попытку.");
                }
            }

            return Ok("Запись успешно изменена!");
        }

        // POST: api/Doctors ONLY ADMIN
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Doctor>> PostDoctor(Doctor doctor)
        {
          if (_context.doctors == null)
          {
              return Problem("Entity set 'DoctorDbContext.doctors'  is null.");
          }
            _context.doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDoctor", new { id = doctor.Id }, doctor);
        }

        // DELETE: api/Doctors/5 ONLY ADMIN
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            if (_context.doctors == null)
            {
                return NotFound("Записей не найдено");
            }
            var doctor = await _context.doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound($"Запись доктора с ID {id} не найдена");
            }

            _context.doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return Ok("Запись успешно удалена!");
        }

        private bool DoctorExists(int id)
        {
            return (_context.doctors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
