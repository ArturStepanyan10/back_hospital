using DocService.Models;
using Microsoft.EntityFrameworkCore;

namespace DocService.Data
{
    public class DoctorDbContext: DbContext
    {
        public DoctorDbContext(DbContextOptions<DoctorDbContext> options) : base(options)
        { }

        public DbSet<Doctor> doctors { get; set; }
    }
}
