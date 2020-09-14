using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AsyncInn.Models;

namespace AsyncInn.Services
{
    public class DatabaseHotelRoomRepo : IHotelRoom
    {
        public Task<Hotel> CreateHotelRoom(Hotel hotel)
        {
            throw new NotImplementedException();
        }

        public Task<Hotel> DeleteOneHotelRoomById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Hotel>> GetAllHotelRooms()
        {
            throw new NotImplementedException();
        }

        public Task<Hotel> GetOneHotelRoom(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateHotelRoomAsync(Hotel hotel)
        {
            throw new NotImplementedException();
        }
    }
}
