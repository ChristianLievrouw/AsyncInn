using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AsyncInn.Models;
using Microsoft.AspNetCore.Mvc;

namespace AsyncInn.Services
{
    public interface IHotelRepo
    {
        Task<IEnumerable<Hotel>> GetAllHotels();

        Task<Hotel> GetOneHotel(int id);

        Task<Hotel> CreateHotel(Hotel hotel);

        Task<bool> UpdateAsync(Hotel hotel);

        Task<Hotel> DeleteOneHotelById(int id);
    }
}
