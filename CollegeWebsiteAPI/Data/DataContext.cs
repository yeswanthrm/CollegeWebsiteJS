using CollegeWebsiteAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CollegeWebsiteAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Registration> Registrations { get; set; }
    }
}
