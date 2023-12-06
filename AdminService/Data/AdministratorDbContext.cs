using AdminService.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminService.Data
{
    public class AdministratorsDbContext : DbContext
    {
        public AdministratorsDbContext(DbContextOptions<AdministratorsDbContext> options) : base(options)
        { }

        public DbSet<Admin> administrators { get; set; }
    }
}