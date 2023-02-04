using API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API.Repositories
{
    public class AidsRepository : RepositoryBase<StudentAid>
    {
        public AidsRepository(Database context) : base(context)
        {
        }

        public override IQueryable<StudentAid> Get()
        {
            return Context.StudentAids
                .Include(p => p.ScienceClub)
                .Include(p => p.User)
                .OrderByDescending(p => p.DateTime);
        }

        public override List<StudentAid> Get(Expression<Func<StudentAid, bool>> expression)
        {
            return Get().AsEnumerable().Where(expression.Compile().Invoke).ToList();
        }
    }
}
