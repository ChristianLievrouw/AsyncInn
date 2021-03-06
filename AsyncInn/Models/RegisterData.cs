﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AsyncInn.Models
{

    public class RegisterData
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public string Phonenumber { get; set; }

        public List<string> Roles { get; set; }
    }
}
