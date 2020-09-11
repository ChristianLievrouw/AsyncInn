using System;
using System.Collections.Generic;

namespace AsyncInn.Models
{
    public class Amenity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // Reverse Navigation Property
        public List<RoomAmenity> RoomAmenities { get; set; }
    }
}
