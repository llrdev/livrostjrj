using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        TEntity FindById(params object[] keyValues);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Find(ISpecification<TEntity> specification = null);
        IEnumerable<TEntity> FindWithInclude(ISpecification<TEntity> specification, string includes, bool isAsNoTracking = false);
        IEnumerable<TEntity> FindWithInclude(ISpecification<TEntity> specification, params string[] includes);
        IEnumerable<TEntity> FindAsNoTracking(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> FindAsNoTracking(ISpecification<TEntity> specification = null);
        IEnumerable<K> FindTransform<K>(Expression<Func<TEntity, K>> selectExpression, Expression<Func<TEntity, bool>> predicate) where K : class;
        IEnumerable<K> FindTransform<K>(Expression<Func<TEntity, K>> selectExpression, ISpecification<TEntity> specification = null) where K : class;
        int Count(ISpecification<TEntity> specification = null);
        int Count(Expression<Func<TEntity, bool>> predicate);
        bool Any(ISpecification<TEntity> specification = null);
        bool Any(Expression<Func<TEntity, bool>> predicate, bool isAsNoTracking = false);
        bool Any(Expression<Func<TEntity, bool>> predicate);
    }
}
