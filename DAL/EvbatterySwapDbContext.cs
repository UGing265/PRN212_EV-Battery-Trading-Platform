using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EvbatterySwapDbContext :DbContext
    {
        public EvbatterySwapDbContext() { }

        public EvbatterySwapDbContext(DbContextOptions<EvbatterySwapDbContext> options): base(options) { }

        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }

        public virtual DbSet<Product>Products { get; set; }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

    }
}
