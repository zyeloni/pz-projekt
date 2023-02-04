using API.Entities;
using API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public class StudentsAidsService
    {
        private readonly AidsRepository aidsRepository;

        public StudentsAidsService(Database context)
        {
            aidsRepository = new AidsRepository(context);
        }

        public List<StudentAid> Get() { return aidsRepository.Get().ToList(); }

        public bool AddAid(StudentAid aid, Guid userID)
        {
            aid.UserID = userID;
            aidsRepository.Create(aid);
            return aidsRepository.Save() > 0;
        }

        public List<StudentAid> Get(Guid guid)
        {
            return aidsRepository.Get(p => p.UserID == guid);
        }

        public List<StudentAid> GetById(Guid guid)
        {
            return aidsRepository.Get(p => p.ID == guid);
        }
    }
}
