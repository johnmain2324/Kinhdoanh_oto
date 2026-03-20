using Microsoft.EntityFrameworkCore;
using Kinhdoanh_oto.Models;
using System.Collections.Generic;
using Kinhdoanh_oto.Core.Models;

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
        public DbSet<Datcoc> Datcoc { get; set; }
        public DbSet<Laithu> Laithu { get; set; }
        public DbSet<Baogia> Baogia { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }
        public DbSet<ServiceBooking> ServiceBookings { get; set; }
    }
}