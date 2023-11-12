using Microsoft.EntityFrameworkCore;
using SheduleService.Models;

namespace SheduleService.Data
{
    public class SheduleDbContext : DbContext
    {
        public SheduleDbContext(DbContextOptions<SheduleDbContext> options) : base(options)
        { }

        public DbSet<Shedule> shedules { get; set; }
    }
}
