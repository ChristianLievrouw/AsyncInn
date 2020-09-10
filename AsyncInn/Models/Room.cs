using System;
using System.ComponentModel.DataAnnotations;

namespace AsyncInn.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public RoomType Type { get; set; }

        public enum RoomType
        {
            Studio,
            OneBedroom,
            TwoBedroom
        }
    }
}
