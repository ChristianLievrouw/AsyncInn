using System;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
