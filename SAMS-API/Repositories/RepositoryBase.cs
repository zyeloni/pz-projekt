using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly Database Context;

        protected RepositoryBase(Database context)
        {
            this.Context = context;
        }

        public virtual void Create(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public virtual IQueryable<T> Get()
        {
            return Context.Set<T>().AsNoTracking();
        }

        public virtual List<T> Get(Expression<Func<T, bool>> expression)
        {
            return Get().AsNoTracking().AsEnumerable().Where(expression.Compile().Invoke).ToList();
        }

        public virtual void Update(T entity)
        {
            Context.Set<T>().Update(entity);
        }

        public virtual int Save()
        {
            return Context.SaveChanges();
        }
    }
}
