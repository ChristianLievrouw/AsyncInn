using System;
using AsyncInn.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using static AsyncInn.Models.Room;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace AsyncInn.Data
{
    public class HotelDbContext : IdentityDbContext<ApplicationUser>
    {
        public HotelDbContext(DbContextOptions options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // We have to have this becuase IdentityDbContext uses it
            base.OnModelCreating(modelBuilder);

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

            modelBuilder.Entity<RoomAmenity>()
                .HasKey(roomAmenity => new
                {
                    roomAmenity.AmenityId,
                    roomAmenity.RoomId
                });

            modelBuilder.Entity<HotelRoom>()
                .HasKey(hotelRoom => new
                {
                    hotelRoom.HotelId,
                    hotelRoom.RoomNumber
                });

            SeedRole(modelBuilder, "Administrator", "ctreate", "update", "delete");
            SeedRole(modelBuilder, "Editor", "create", "update");
        }
        private int nextRoleClaimId = 1;

        private void SeedRole(ModelBuilder modelBuilder, string roleName, params string[] permissions)
        {
            var role = new IdentityRole
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString(),
            };
            modelBuilder.Entity<IdentityRole>()
                .HasData(role);

            var roleClaims = permissions
                .Select(permission =>
                new IdentityRoleClaim<string>
                {
                    Id = nextRoleClaimId++,
                    RoleId = role.Id,
                    ClaimType = "permissions",
                    ClaimValue = permission,
                })
                .ToArray();

            modelBuilder.Entity<IdentityRoleClaim<string>>()
                .HasData(roleClaims);
        }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Amenity> Amenitites { get; set; }

        public DbSet<RoomAmenity> RoomAmenities { get; set; }

        public DbSet<HotelRoom> HotelRooms { get; set; }
    }
}
