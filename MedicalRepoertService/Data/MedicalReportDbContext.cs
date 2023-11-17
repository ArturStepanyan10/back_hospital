using MedicalReportService.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalReportService.Data
{
    public class MedicalReportDbContext : DbContext
    {
        public MedicalReportDbContext(DbContextOptions<MedicalReportDbContext> options) : base(options)
        { }

        public DbSet<MedicalReport> MedicalReports { get; set; }
    }
}
