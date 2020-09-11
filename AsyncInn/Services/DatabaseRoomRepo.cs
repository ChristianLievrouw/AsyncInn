using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncInn.Data;
using AsyncInn.Models;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn.Services
{
    public class DatabaseRoomRepo : IRoomRepo
    {
        private readonly HotelDbContext _context;

        public DatabaseRoomRepo(HotelDbContext context)
        {
            _context = context;
        }

        public Task<Room> AddAmenityToRoom(int roomId, int amenityId)
        {
            throw new NotImplementedException();
        }

        public async Task<Room> CreateOneRoom(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public Task<Room> DeleteAmenityFromRoom(int roomId, int amenityId)
        {
            throw new NotImplementedException();
        }

        public async Task<Room> DeleteOneRoomById(int id)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
            {
                return null;
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return room;
        }

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> GetOneRoom(int id)
        {
            return await _context.Rooms.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Room room)
        {
            _context.Entry(room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RoomExistsAsync(room.Id))
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

        private async Task<bool> RoomExistsAsync(int id)
        {
            return await _context.Rooms.AnyAsync(e => e.Id == id);
        }
    }
}
