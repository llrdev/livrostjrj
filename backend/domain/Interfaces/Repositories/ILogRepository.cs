using System;
using System.Collections.Generic;

namespace Domain.Interfaces.Repositories
{
    public interface ILogRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        IEnumerable<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        void SetVerbose();
    }
}
