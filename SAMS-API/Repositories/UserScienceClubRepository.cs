using API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API.Repositories
{
    public class UserScienceClubRepository : RepositoryBase<UserScienceClub>
    {
        public UserScienceClubRepository(Database context) : base(context)
        {
        }

        public override IQueryable<UserScienceClub> Get()
        {
            return base.Get()
                .Include(p => p.User)
                .Include(p => p.ScienceClub);
        }
    }
}
