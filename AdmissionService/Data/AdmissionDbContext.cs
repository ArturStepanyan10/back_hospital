using AdmissionService.Models;
using Microsoft.EntityFrameworkCore;

namespace AdmissionService.Data
{
    public class AdmissionDbContext: DbContext
    {
        public AdmissionDbContext(DbContextOptions<AdmissionDbContext> options) : base(options)
        { }

        public DbSet<Admission> admissions { get; set; }
    }
}
