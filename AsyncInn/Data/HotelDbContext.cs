using System;
using AsyncInn.Models;
using Microsoft.EntityFrameworkCore;
using static AsyncInn.Models.Room;

namespace AsyncInn.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Hotel>()
                .HasData(
                    new Hotel { Id = 1, Name = "Async Inn", Address = "123 Sync Street", City = "Des Moines", State = "Iowa", Phone = 515-555-1234 },
                    new Hotel { Id = 2, Name = "Async Motel", Address = "1858 Cyclone Lane", City = "Ames", State = "Iowa", Phone = 515-294-1111 },
                    new Hotel { Id = 3, Name = "Async Suites", Address = "5050 Morning Star Court", City = "Pleasant Hill", State = "Iowa", Phone = 515-299-1234 }
                );
            modelBuilder.Entity<Room>()
                .HasData(
                    new Room { Id = 1, Name = "Studio", Type = (RoomType)2 },
                    new Room { Id = 2, Name = "OneBedroom", Type = (RoomType)1 },
                    new Room { Id = 3, Name = "TwoBedroom", Type = 0 }
                );
            modelBuilder.Entity<Amenity>()
                 .HasData(
                    new Amenity { Id = 1, Name = "WiFi" },
                    new Amenity { Id = 2, Name = "Air Conditioning" },
                    new Amenity { Id = 3, Name = "Coffee Maker" }
                );
        }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Amenity> Amenitites { get; set; }
    }
}
