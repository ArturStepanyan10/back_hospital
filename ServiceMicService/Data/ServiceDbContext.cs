using Microsoft.EntityFrameworkCore;
using ServiceMicService.Model;
using System.Collections.Generic;

namespace ServiceMicService.Data
{ 
    public class ServiceDbContext : DbContext
    {
        public ServiceDbContext(DbContextOptions<ServiceDbContext> options) : base(options)
        { }

        public DbSet<Service> services { get; set; }
    }
    
}
