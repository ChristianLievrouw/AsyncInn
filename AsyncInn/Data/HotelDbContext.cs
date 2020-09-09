using System;
using AsyncInn.Models;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
    }
}
