using API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API.Repositories
{
    public class ScienceClubRepository : RepositoryBase<ScienceClub>
    {
        public ScienceClubRepository(Database context) : base(context)
        {
        }

        public override IQueryable<ScienceClub> Get()
        {
            return Context.ScienceClubs.AsNoTracking();
        }
    }
}
