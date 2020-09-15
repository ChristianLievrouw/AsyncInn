using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AsyncInn.Data;
using AsyncInn.Models;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn.Services
{
    public class DatabaseHotelRoomRepo : IHotelRoom
    {
        private readonly HotelDbContext _context;

        public DatabaseHotelRoomRepo(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<HotelRoom> CreateHotelRoom(HotelRoom hotelRoom)
        {
            _context.HotelRooms.Add(hotelRoom);
            await _context.SaveChangesAsync();
            return hotelRoom;
        }

        public async Task<HotelRoom> DeleteOneHotelRoomById(int id)
        {
            var hotelRoom = await _context.HotelRooms.FindAsync(id);

            if(hotelRoom == null)
            {
                return null;
            }

            _context.HotelRooms.Remove(hotelRoom);
            await _context.SaveChangesAsync();
            return hotelRoom;
        }

        public async Task<IEnumerable<HotelRoom>> GetAllHotelRooms()
        {
            return await _context.HotelRooms.ToListAsync();
        }

        public async Task<HotelRoom> GetOneHotelRoom(int id)
        {
            var hotelRoom = await _context.HotelRooms.FindAsync(id);
            return hotelRoom;
        }

        public async Task<bool> UpdateHotelRoomAsync(HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!await HotelRoomExists(hotelRoom.RoomId))
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

        private async Task<bool> HotelRoomExists(int id)
        {
            return await _context.HotelRooms.AnyAsync(e => e.HotelId == id);
        }
    }
}
