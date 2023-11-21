using CompositeService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using SheduleService.Models;
using AdmissionService.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;


namespace CompositeService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompositeController : ControllerBase
    {
        private readonly string _doctorServiceAddress = "https://localhost:7025/api/doctors";
        private readonly string _sheduleServiceAddress = "https://localhost:7163/api/shedules";
        

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

        //Возвращает список докторов в заданный день недели
        [HttpGet("shedules/{dayofweek}")]
        public async Task<ActionResult<List<SheduleCos>>> GetDoctorScheduleByDayAsync(int dayofweek)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (HttpClient client = new HttpClient(clientHandler))
            {
                HttpResponseMessage response = await client.GetAsync($"{_sheduleServiceAddress}");
                if (response.IsSuccessStatusCode)
                {
                    List<SheduleCos> shedules = await response.Content.ReadFromJsonAsync<List<SheduleCos>>();

                    DayOfWeek targetDay = (DayOfWeek)dayofweek;

                    List<int> doctorIdsWithSchedule = shedules
                        .Where(schedule => schedule.DayOfWeek == targetDay)
                        .Select(schedule => schedule.DoctorId)
                        .Distinct()
                        .ToList();

                    var doctorsWithScheduleInfo = doctorIdsWithSchedule
                .Select(async doctorId =>
                {
                    response = await client.GetAsync($"{_doctorServiceAddress}/{doctorId}");
                    if (response.IsSuccessStatusCode)
                    {
                        Doctor doctor = await response.Content.ReadFromJsonAsync<Doctor>();
                        if (doctor != null)
                        {
                            return new
                            {
                                doctor.Surname,
                                doctor.Name,
                                doctor.SpecName
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
        [HttpGet("myschedule/{doctorId}")]
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

    }
}


