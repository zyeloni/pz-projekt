using API.Entities;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Models;

namespace API.Services
{
    public class UsersService
    {

        private readonly UsersRepository usersRepository;
        private readonly UserScienceClubRepository uscRepository;
        private readonly AuthService authService;

        public UsersService(AuthService authService, Database context)
        {
            this.authService = authService;
            usersRepository = new UsersRepository(context);
            this.uscRepository = new UserScienceClubRepository(context);
        }

        public KeyValuePair<string, object> Login(LoginUser authUser, bool website = false)
        {

            var user = usersRepository.Get(p => p.Email == authUser.Email).FirstOrDefault();

            if (user == null)
                return new KeyValuePair<string, object>("username", "Wrong email or user does not exists");

            if (user.Password != authUser.Password)
                return new KeyValuePair<string, object>("password", "Wrong password");

            try
            {
                var result = authService.GenerateToken(user);
                user.RefreshToken = result.Refresh;
                usersRepository.Update(user);
                var b = usersRepository.Save() > 0;
                if (b) return new KeyValuePair<string, object>("ok", result);
                else throw new Exception("Wrong data while generating token");
            }
            catch (Exception ex)
            {
                return new KeyValuePair<string, object>("error", $"{ex.Message}");
            }

        }

        public KeyValuePair<string, object> Refresh(TokenResponse tokenResponse)
        {
            var user = usersRepository.Get(u => u.ID.Equals(Guid.Parse(tokenResponse.ID)) && u.RefreshToken.Equals(tokenResponse.Refresh)).FirstOrDefault();
            if (user==null)
                return new KeyValuePair<string, object>("error", "Wrong data");

            var token = authService.GenerateToken(user);
            user.RefreshToken = token.Refresh;
            usersRepository.Update(user);
            var b = usersRepository.Save() > 0;

            if (b) return new KeyValuePair<string, object>("token", token);
            else throw new Exception("Wrong data while generating token");
        }

        public User Profile(Guid id)
        {
            return usersRepository.Get(p => p.ID == id).FirstOrDefault();
        }

        public KeyValuePair<string, object> Register(RegisterUser user)
        {
            if (usersRepository.Get(p => p.Email == user.Email).FirstOrDefault() != null)
                return new KeyValuePair<string, object>("error", "Email already registered");

            try
            {
                User u = new User(user);
                usersRepository.Create(u);
                usersRepository.Save();
                foreach (Guid club in user.clubs)
                {
                    uscRepository.Create(new UserScienceClub() { Active = true, ScienceClubID = club, UserID = u.ID });
                    uscRepository.Save();
                }
                return new KeyValuePair<string, object>("ok", "User registered");
            }
            catch (Exception ex)
            {
                return new KeyValuePair<string, object>("error", $"Unhandled error: {ex.Message}");
            }
        }
    }
}
