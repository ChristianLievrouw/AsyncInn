using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsyncInn.Models
{
    public class RoomAmenity
    {
        public int AmenityId { get; set; }

        public int RoomId { get; set; }

        [ForeignKey(nameof(AmenityId))]
        public Amenity Amenity { get; set; }

        [ForeignKey(nameof(RoomId))]
        public Room Room { get; set; }
    }
}
