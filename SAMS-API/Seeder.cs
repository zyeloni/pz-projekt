using API.Entities;
using System;
using System.Linq;

namespace API
{
    public class Seeder
    {
        private readonly Database context;

        public Seeder(Database context)
        {
            this.context = context;
        }

        public void Seed()
        {
            if (!context.Database.CanConnect()) return;
            
            
            if (!context.ScienceClubs.Any()) SeedClubs();
            if (!context.Users.Any()) SeedUsers();
            if (!context.UserScienceClubs.Any()) SeedUserScienceClubs();
        }

        private void SeedUserScienceClubs()
        {
            context.UserScienceClubs
                .Add(new UserScienceClub() 
                    { User = context.Users.First(), ScienceClub = context.ScienceClubs.First(), Active = true }
                );
            context.SaveChanges();
        }

        private void SeedClubs()
        {
            context.ScienceClubs.Add(new ScienceClub() { Name = "KNSI GenBit" });
            context.SaveChanges();
        }

        private void SeedUsers()
        {
            context.Users.Add(
                new User() { 
                    Name = "Hubert", 
                    Surname = "Troć", 
                    Email = "psptorchinim@gmail.com", 
                    Password = "testPassword1234",
                    Registered = DateTimeOffset.Now,
                    Role = "Admin"
                });
            context.SaveChanges();
        }
    }
}
