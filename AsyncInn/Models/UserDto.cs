using System;
using System.Collections.Generic;

namespace AsyncInn.Models
{
    public class UserDto
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Token { get; set; }

        public IList<string> Roles { get; set; }
    }
}
