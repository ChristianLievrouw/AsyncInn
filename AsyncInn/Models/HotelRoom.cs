using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsyncInn.Models
{
    public class HotelRoom
    {
       
        public long HotelId { get; set; }

        public int RoomNumber { get; set; }
     
        public int RoomId { get; set; }

        [Column(TypeName = "money")]
        public decimal Rate { get; set; }

        public bool PetFriendly { get; set; }

        [ForeignKey(nameof(HotelId))]
        public Hotel Hotel { get; set; }

        [ForeignKey(nameof(RoomId))]
        public Room Room { get; set; }
    }
}
