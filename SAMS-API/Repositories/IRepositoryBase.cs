using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API.Repositories
{
    public interface IRepositoryBase<T>
    {
        void Create(T entity);
        void Delete(T entity);
        IQueryable<T> Get();
        List<T> Get(Expression<Func<T, bool>> expression);
        void Update(T entity);
        int Save();
    }
}