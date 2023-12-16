using CompositeService.Models;
using Microsoft.AspNetCore.Mvc;
using SheduleService.Models;


namespace CompositeService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompositeController : ControllerBase
    {
        private readonly string _doctorServiceAddress = "https://localhost:7025/api/doctors";
        private readonly string _sheduleServiceAddress = "https://localhost:7163/api/shedules";
        private readonly string _patientServiceAddress = "https://localhost:44373/api/patients";


        //Возвращает список докторов заданной специализации
        [HttpGet("doctors/{SpecName}")]
        public async Task<List<Doctor>> GetDoctorBySpecialAsync(string SpecName)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpClient client = new HttpClient(clientHandler))
            {
                HttpResponseMessage response = await client.GetAsync($"{_doctorServiceAddress}");
                if (response.IsSuccessStatusCode)
                {
                    List<Doctor> doctors = await response.Content.ReadFromJsonAsync<List<Doctor>>();

                    return doctors.Where(doctor => doctor.SpecName == SpecName).ToList();
                }
            }
            return null;
        }

        //Возвращает список докторов в заданный день 
        [HttpGet("shedules/{date}")]
        public async Task<ActionResult<List<Doctor>>> GetDoctorScheduleByDateAsync(string date)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (HttpClient client = new HttpClient(clientHandler))
            {
                HttpResponseMessage response = await client.GetAsync($"{_sheduleServiceAddress}");
                if (response.IsSuccessStatusCode)
                {
                    List<SheduleCos> shedules = await response.Content.ReadFromJsonAsync<List<SheduleCos>>();

                    // Фильтрация расписания по заданной дате
                    List<SheduleCos> targetDateSchedules = shedules
                        .Where(schedule => schedule.Date == date)
                        .ToList();

                    List<int> doctorIdsWithSchedule = targetDateSchedules
                        .Select(schedule => schedule.DoctorId)
                        .Distinct()
                        .ToList();

                    // Запрос информации о докторах
                    var doctorsWithScheduleInfo = doctorIdsWithSchedule
                            .Select(async doctorId =>
                    {
                            response = await client.GetAsync($"{_doctorServiceAddress}/{doctorId}");
                            if (response.IsSuccessStatusCode)
                            {
                                Doctor doctor = await response.Content.ReadFromJsonAsync<Doctor>();
                                if (doctor != null)
                                {
                                    return new Doctor
                                    {
                                        Surname = doctor.Surname,
                                        Name = doctor.Name,
                                        Experience = doctor.Experience,
                                        Post = doctor.Post,
                                        SpecName = doctor.SpecName
                                    };
                                }
                            }

                            return null;
                        })

                    .Where(doctorTask => doctorTask != null)
                    .Select(doctorTask => doctorTask.Result)
                    .ToList();

                    return Ok(doctorsWithScheduleInfo);
                }
            }

            return null;
        }


        //Просмотр расписания доктора
        [HttpGet("scheduleDoc/{doctorId}")]
        public async Task<ActionResult<List<Shedule>>> GetMyScheduleAsync(int doctorId)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (HttpClient client = new HttpClient(clientHandler))
            {
                HttpResponseMessage response = await client.GetAsync($"{_sheduleServiceAddress}");
                if (response.IsSuccessStatusCode)
                {
                    List<Shedule> allSchedules = await response.Content.ReadFromJsonAsync<List<Shedule>>();

                    // Фильтрация расписания по ID врача
                    List<Shedule> mySchedule = allSchedules
                        .Where(schedule => schedule.DoctorId == doctorId)
                        .ToList();

                    return mySchedule;
                }
            }

            return null;
        }

        //Возвращает пациентов заданного доктора
        [HttpGet("patients/{doctorId}")]
        public async Task<List<PatientComp>> GetPatientsByDoctorAsync(int doctorId)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback =
            (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpClient client = new HttpClient(clientHandler))
            {
                HttpResponseMessage response = await
                client.GetAsync($"{_patientServiceAddress}");
                if (response.IsSuccessStatusCode)
                {
                    List<PatientComp> patients = await response.Content.ReadFromJsonAsync<List<PatientComp>>();
                    return patients.Where(patients => patients.DoctorId
                    == doctorId).ToList();
                }
            }
            return null;
        }

    }
}



