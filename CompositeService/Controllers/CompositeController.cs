using CompositeService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;


namespace CompositeService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompositeController : ControllerBase
    {
        private readonly string _doctorServiceAddress = "https://localhost:7025/api/doctors";
        //private readonly string _specializationServiceAddress = "https://localhost:7138/api/specializations";

        
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



    }
}

