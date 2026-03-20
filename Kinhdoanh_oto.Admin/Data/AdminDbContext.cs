using Microsoft.EntityFrameworkCore;
using Kinhdoanh_oto.Admin.Models;

namespace Kinhdoanh_oto.Admin.Data
{
    public class AdminDbContext : DbContext
    {
        public AdminDbContext(DbContextOptions<AdminDbContext> options)
            : base(options)
        {
        }

        public DbSet<Nguoiquantri> Nguoiquantris { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Tintuc> Tintuc { get; set; }
        public DbSet<Khachhang> Khachhang { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Nguoiquantri>()
                .ToTable("Nguoiquantri");
        }
    }
}