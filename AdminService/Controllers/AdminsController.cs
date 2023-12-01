using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdminService.Data;  
using DocService.Data;
using DocService.Models;

[Route("api/admin/administrators")]
[ApiController]
public class AdminAdministratorsController : ControllerBase
{
    private readonly AdminDbContext _adminDbContext;
    private readonly DoctorDbContext _docDbContext;

    public AdminAdministratorsController(AdminDbContext adminDbContext, DoctorDbContext docDbContext)
    {
        _adminDbContext = adminDbContext;
        _docDbContext = docDbContext;
    }

    // Получить список всех врачей
    [HttpGet("doctors")]
    public IActionResult GetDoctors()
    {
        var doctors = _docDbContext.doctors.ToList();
        return Ok(doctors);
    }

    // Добавить нового врача
    [HttpPost("doctors")]
    public IActionResult AddDoctor([FromBody] Doctor doctor)
    {
        if (ModelState.IsValid)
        {
            _docDbContext.doctors.Add(doctor);
            _docDbContext.SaveChanges();
            return CreatedAtAction(nameof(GetDoctors), new { id = doctor.Id }, doctor);
        }

        return BadRequest(ModelState);
    }

    // Редактировать существующего врача
    [HttpPut("doctors/{id}")]
    public IActionResult UpdateDoctor(int id, [FromBody] Doctor doctor)
    {
        if (id != doctor.Id)
        {
            return BadRequest();
        }

        _docDbContext.Entry(doctor).State = EntityState.Modified;

        try
        {
            _docDbContext.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DoctorExists(id))
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

    // Удалить врача
    [HttpDelete("doctors/{id}")]
    public IActionResult DeleteDoctor(int id)
    {
        var doctor = _docDbContext.doctors.Find(id);

        if (doctor == null)
        {
            return NotFound();
        }

        _docDbContext.doctors.Remove(doctor);
        _docDbContext.SaveChanges();

        return NoContent();
    }

    private bool DoctorExists(int id)
    {
        return _docDbContext.doctors.Any(e => e.Id == id);
    }
}
