using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncInn.Data;
using AsyncInn.Models;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn.Services
{
    public class DatabaseAmenityRepo : IAmenityRepo
    {
        private readonly HotelDbContext _context;

        public DatabaseAmenityRepo(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<Amenity> CreateOneAmenity(Amenity amenity)
        {
            _context.Amenitites.Add(amenity);
            await _context.SaveChangesAsync();
            return amenity;
        }

        public async Task<Amenity> DeleteOneAmenityById(int id)
        {
            var amenity = await _context.Amenitites.FindAsync(id);

            if (amenity == null)
            {
                return null;
            }

            _context.Amenitites.Remove(amenity);
            await _context.SaveChangesAsync();

            return amenity;
        }

        public async Task<IEnumerable<Amenity>> GetAllAmenities()
        {
            return await _context.Amenitites.ToListAsync();
        }

        //public async Task<IEnumerable<Amenity>> GetAllHotels()
        //{
        //    return await _context.Amenitites.ToListAsync();
        //}

        public async Task<Amenity> GetOneAmenity(int id)
        {
            return await _context.Amenitites.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Amenity amenity)
        {
            _context.Entry(amenity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AmenityExistsAsync(amenity.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        private async Task<bool> AmenityExistsAsync(int id)
        {
            return await _context.Amenitites.AnyAsync(e => e.Id == id);
        }
    }
}
