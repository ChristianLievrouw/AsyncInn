using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AsyncInn.Models;

namespace AsyncInn.Services
{
    public interface IHotelRoom
    {
        Task<IEnumerable<Hotel>> GetAllHotelRooms();

        Task<Hotel> GetOneHotelRoom(int id);

        Task<Hotel> CreateHotelRoom(Hotel hotel);

        Task<bool> UpdateHotelRoomAsync(Hotel hotel);

        Task<Hotel> DeleteOneHotelRoomById(int id);
    }
}
