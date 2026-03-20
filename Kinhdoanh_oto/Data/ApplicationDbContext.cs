using Microsoft.EntityFrameworkCore;
using Kinhdoanh_oto.Models;
using System.Collections.Generic;

namespace Kinhdoanh_oto.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Khachhang> Khachhang { get; set; }
        public DbSet<OTPDangky> OTPDangky { get; set; }
        public DbSet<Car> Cars { get; set; }
    }
}