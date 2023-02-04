using API.Models;
using System;
using System.Collections.Generic;

namespace API.Entities
{
    public class User
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTimeOffset Registered { get; set; }
        public string RefreshToken { get; set; }
        public string Role { get; set; }

        public List<UserScienceClub> Clubs { get; set; }
        public List<StudentAid> StudentAids { get; set;}

        public User() { }

        public User(RegisterUser user)
        {
            Name = user.Name;
            Surname = user.Surname;
            Email = user.Email;
            Password = user.Password;
            Registered = DateTimeOffset.Now;
            Role = "Normal User";
        }
    }
}
