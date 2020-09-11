using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncInn.Data;
using AsyncInn.Models;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn.Services
{
    public class DatabaseHotelRepo : IHotelRepo
    {
        private readonly HotelDbContext _context;

        public DatabaseHotelRepo(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<Hotel> CreateHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task<Hotel> DeleteOneHotelById(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            if(hotel == null)
            {
                return null;
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            return hotel;
        }

        public async Task<IEnumerable<Hotel>> GetAllHotels()
        {
            return await _context.Hotels.ToListAsync();
        }

        public async Task<Hotel> GetOneHotel(int id)
        {
            return await _context.Hotels.FindAsync(id);
           
        }

        public async Task<bool> UpdateAsync(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists((int)hotel.Id))
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
    

        private async Task<bool> HotelExists(int id)
        {
           return await _context.Hotels.AnyAsync(e => e.Id == id);
        }
    }
}
