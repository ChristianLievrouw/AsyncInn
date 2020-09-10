using System;
using System.ComponentModel.DataAnnotations;

namespace AsyncInn.Models
{
    public class Hotel
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        public string Country { get; set; }

        public int Phone { get; set; }
    }
}
