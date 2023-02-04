using API.Entities;
using API.Models;
using API.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public class ScienceClubsService
    {
        private readonly ScienceClubRepository clubRepository;
        private readonly UserScienceClubRepository uscRepository;

        public ScienceClubsService(Database context)
        {
            this.clubRepository = new ScienceClubRepository(context);
            this.uscRepository = new UserScienceClubRepository(context);
        }

        public List<ScienceClub> Get()
        {
            return clubRepository.Get().ToList();
        }

        public bool AddClub(AddClub club, Guid uid)
        {
            var c = new ScienceClub() { Name = club.Name };
            clubRepository.Create(c);
            var added = clubRepository.Save() > 0;
            if (club.Join)
            {
                uscRepository.Create(new UserScienceClub() { Active = true, ScienceClubID = c.ID, UserID = uid});
                added = added && uscRepository.Save() > 0;
            }

            return added;
        }

        public bool Leave(Guid userID, Guid clubID)
        {
            var c = uscRepository.Get(p => p.ScienceClub.ID == clubID && p.User.ID == userID).FirstOrDefault();
            if (c != null)
            {
                c.Active = false;
                uscRepository.Update(c);
            }
                
            int res =  clubRepository.Save();
            return res > 0;
        }

        public bool Join(Guid userID, Guid clubID)
        {
            var c = uscRepository.Get(p => p.ScienceClub.ID == clubID && p.User.ID == userID).FirstOrDefault();
            if (c != null)
            {
                c.Active = true;
                uscRepository.Update(c);
            }  
            else if (c == null)
                uscRepository.Create(new UserScienceClub()
                 {
                     Active = true,
                     ScienceClubID = clubID,
                     UserID = userID
                 });
            return uscRepository.Save() > 0;
        }
    }
}
