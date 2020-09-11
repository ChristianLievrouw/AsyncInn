using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AsyncInn.Models;

namespace AsyncInn.Services
{
    public interface IRoomRepo
    {
        Task<IEnumerable<Room>> GetAllRooms();

        Task<Room> GetOneRoom(int id);

        Task<Room> CreateOneRoom(Room room);

        Task<bool> UpdateAsync(Room room);

        Task<Room> DeleteOneRoomById(int id);
    }
}
