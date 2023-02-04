using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.Entities;

namespace API.Repositories
{
    public class UsersRepository : RepositoryBase<User>
    {
        public UsersRepository(Database context) : base(context)
        {
        }

        public override IQueryable<User> Get()
        {
            return Context.Users
                .Include(p => p.Clubs)
                .ThenInclude(p => p.ScienceClub);
        }
    }
}
