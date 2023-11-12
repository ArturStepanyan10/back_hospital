using Microsoft.EntityFrameworkCore;
using ServiceMicService.Models;

namespace ServiceMicService.Data
{
    public class ServiceDbContext: DbContext
    {
        public ServiceDbContext(DbContextOptions<ServiceDbContext> options) : base(options)
        { }

        public DbSet<Service> services { get; set; }
    }
}
