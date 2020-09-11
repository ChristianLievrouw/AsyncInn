using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AsyncInn.Models;

namespace AsyncInn.Services
{
    public interface IAmenityRepo
    {
        Task<IEnumerable<Amenity>> GetAllAmenities();

        Task<Amenity> GetOneAmenity(int id);

        Task<Amenity> CreateOneAmenity(Amenity amenity);

        Task<bool> UpdateAsync(Amenity amenity);

        Task<Amenity> DeleteOneAmenityById(int id);
    }
}
