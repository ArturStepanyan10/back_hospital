using CompositeService.Models;
using DocService.Models;
using Microsoft.AspNetCore.Mvc;
using SheduleService.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CompositeService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompositeController : ControllerBase
    {
        private readonly string _docSerAddress = "https://localhost:7025/api/doctors";
        private readonly string _admSerAddress = "https://localhost:7140/api/admissions";
        private readonly string _serSerAddress = "https://localhost:7222/api/services";
        private readonly string _shedSerAddress = "https://localhost:7163/api/shedules";
        private readonly string _specSerAddress = "https://localhost:7138/api/specializations";

        [HttpGet("shedule/{doctorId}")]
        public async Task<ActionResult<CompositeDocShed>> GetScheduleForDoctor(int doctorId)
        {
            // Выполнить запрос к микросервису Doctor для получения информации о враче
            Doctor doctor;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{_docSerAddress}/{doctorId}");

                if (!response.IsSuccessStatusCode)
                {
                    return NotFound(); // Врач не найден
                }

                var content = await response.Content.ReadAsStringAsync();
                doctor = JsonSerializer.Deserialize<Doctor>(content);
            }

            // Выполнить запрос к микросервису Schedule для получения расписания врача
            Shedule shedule;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{_shedSerAddress}/{doctorId}");

                if (!response.IsSuccessStatusCode)
                {
                    return NotFound(); // Расписание не найдено
                }

                var content = await response.Content.ReadAsStringAsync();
                shedule = JsonSerializer.Deserialize<Shedule>(content);
            }

            // Создать объект CompositeDoctorSchedule, объединяющий информацию о враче и его расписании
            var compositeDoctorSchedule = new CompositeDocShed
            {
                DoctorId = doctor.Id,
                DoctorName = $"{doctor.Surname} {doctor.Name}",
                Experience = doctor.Experience,
                Post = doctor.Post,
                SpecializationId = doctor.SpecializationId,
                //Shedules = shedule
            };

            return compositeDoctorSchedule;
        }

    }

}