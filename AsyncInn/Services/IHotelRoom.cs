using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AsyncInn.Models;

namespace AsyncInn.Services
{
    public interface IHotelRoom
    {
        Task<IEnumerable<HotelRoom>> GetAllHotelRooms();

        Task<HotelRoom> GetOneHotelRoom(int id);

        Task<HotelRoom> CreateHotelRoom(HotelRoom hotelRoom);

        Task<bool> UpdateHotelRoomAsync(HotelRoom hotelRoom);

        Task<HotelRoom> DeleteOneHotelRoomById(int id);
    }
}
