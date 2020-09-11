using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsyncInn.Models
{
    public class RoomAmenities
    {
        public long AmenityId { get; set; }

        public long RoomId { get; set; }

        [ForeignKey(nameof(AmenityId))]
        public Amenity Amenity { get; set; }

        [ForeignKey(nameof(RoomId))]
        public Room Room { get; set; }
    }
}
