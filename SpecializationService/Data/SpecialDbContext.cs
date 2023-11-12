using Microsoft.EntityFrameworkCore;
using SpecializationService.Models;

namespace SpecializationService.Data
{
    public class SpecialDbContext: DbContext
    {
        public SpecialDbContext(DbContextOptions<SpecialDbContext> options) : base(options)
        { }

        public DbSet<Specialization> specials { get; set; }
    }
}
