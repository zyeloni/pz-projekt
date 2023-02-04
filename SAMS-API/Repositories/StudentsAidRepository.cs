using API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API.Repositories
{
    public class StudentsAidRepository : RepositoryBase<StudentAid>
    {
        public StudentsAidRepository(Database context) : base(context)
        {
        }

        public override IQueryable<StudentAid> Get()
        {
            return Context.StudentAids.AsNoTracking()
                .Include(p => p.User).AsNoTracking()
                .Include(p => p.ScienceClub).AsNoTracking();
        }
    }
}
