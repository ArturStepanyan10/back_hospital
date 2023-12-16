using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdmissionService.Data;
using AdmissionService.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using SheduleService.Models;
using SheduleService.Data;


namespace AdmissionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionsController : ControllerBase
    {
        private readonly AdmissionDbContext _context;
        private readonly SheduleDbContext _shedContext;

        public AdmissionsController(AdmissionDbContext context, SheduleDbContext shedContext)
        {
            _context = context;
            _shedContext = shedContext;
            
        }

        // GET: api/Admissions ONLY DOCTOR AND ADMIN
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admission>>> Getadmissions()
        {
          if (_context.admissions == null)
          {
              return NotFound("Записей не найдено");
          }
            return await _context.admissions.ToListAsync();
        }

        // GET: api/Admissions/5 ONLY DOCTOR AND ADMIN
        [HttpGet("{id}")]
        public async Task<ActionResult<Admission>> GetAdmission(int id)
        {
          if (id <= 0) 
          {
                return BadRequest("Неверный идентификатор!");
          }

            try
            {
                if (_context.admissions == null)
                {
                    return NotFound("Записи приема не найдены");
                }
                var admission = await _context.admissions.FindAsync(id);

                if (admission == null)
                {
                    return NotFound($"Запись приема с ID {id} не найдена");
                }

                return admission;
            }

            catch (Exception)
            {
                // Логирование ошибки или другие действия по обработке ошибки
                return StatusCode(500, "Internal Server Error");
            }
        }

        // PUT: api/Admissions/5 ONLY ADMIN AND PATIENT
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmission(int id, Admission admission)
        {
            if (id != admission.Id)
            {
                return BadRequest($"Данное ID {id} не найдено!");
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
                    return NotFound($"Запись приема с ID {id} не найдена");
                }
                else
                {
                    return StatusCode(409, "Произошел конфликт. Пожалуйста, обновите данные и повторите попытку.");
                }
            }

            return Ok("Запись успешно изменена!");
        }

      

        private async Task<bool> IsTimeSlotAvailableForPatient(int patientId, int doctorId, string time)
        {
            // Проверяем в базе данных, есть ли запись с указанным временем у данного врача для данного пациента
            return await _context.admissions
                .Where(a => a.PatientId == patientId && a.DoctorId == doctorId && a.Time == time)
                .FirstOrDefaultAsync() == null;
        }
        
        
        //ONLY PATIENT
        //ЗАПИСЬ НА ПРИЕМ 
        [HttpPost]
        public async Task<IActionResult> MakeAddmission([FromBody] Admission admission)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (_context.admissions == null)
                    {
                        return Problem("Entity set 'AdmissionDbContext.admissions'  is null.");
                    }

                    bool isTimeSlotAvailable = await IsTimeSlotAvailableForPatient(admission.PatientId, admission.DoctorId, admission.Time);

                    if (!isTimeSlotAvailable)
                    {
                        // Время занято, возвращаем ошибку
                        return BadRequest("Выбранное время уже занято у данного врача.");
                    }

                    // Время свободно, добавляем запись
                    _context.admissions.Add(admission);
                    await _context.SaveChangesAsync();

                    var schedule = await _shedContext.shedules
                        .FirstOrDefaultAsync(s => s.DoctorId == admission.DoctorId);

                    
                    if (schedule.AdmissionId == null)
                    {
                        schedule.AdmissionId = new List<int>();
                    }
                    schedule.AdmissionId.Add(admission.Id);
                    _shedContext.shedules.Update(schedule);

                    
                    await _shedContext.SaveChangesAsync();
                    
                    await _context.SaveChangesAsync();

                    transaction.Commit();

                    return Ok("Вы записаны на прием!");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(500, $"Произошла ошибка при записи на прием: {ex.Message}");
                }
            }
        }


        // DELETE: api/Admissions/5 ONLY ADMIN
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmission(int id)
        {
            if (_context.admissions == null)
            {
                return NotFound("Записей не найдено");
            }
            var admission = await _context.admissions.FindAsync(id);
            if (admission == null)
            {
                return NotFound($"Запись приема с ID {id} не найдена");
            }

            _context.admissions.Remove(admission);
            await _context.SaveChangesAsync();

            return Ok("Запись успешно удалена!");
        }

        private bool AdmissionExists(int id)
        {
            return (_context.admissions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
