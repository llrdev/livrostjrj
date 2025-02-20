using Domain.Core;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Infra.Data.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Infra.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }
        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {

            //_context.Set<TEntity>().Attach(entity);
            //_context.Entry(entity).State = EntityState.Modified;
            _context.Set<TEntity>().Update(entity);
        }

        public TEntity FindById(params object[] keyValues)
        {
            var entity = _context.Set<TEntity>().Find(keyValues);
            if (entity != null) _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public IEnumerable<TEntity> Find(ISpecification<TEntity> specification = null)
        {
            return ApplySpecification(specification);
        }
        public IEnumerable<TEntity> FindAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).AsNoTracking();
        }

        public IEnumerable<TEntity> FindAsNoTracking(ISpecification<TEntity> specification = null)
        {
            return ApplySpecification(specification).AsNoTracking();
        }

        public IEnumerable<K> FindTransform<K>(Expression<Func<TEntity, K>> selectExpression, Expression<Func<TEntity, bool>> predicate) where K : class
        {
            return _context.Set<TEntity>().Where(predicate).Select(selectExpression);
        }

        public IEnumerable<K> FindTransform<K>(Expression<Func<TEntity, K>> selectExpression, ISpecification<TEntity> specification = null) where K : class
        {
            return ApplySpecification(specification).Select(selectExpression);
        }

        public int Count(ISpecification<TEntity> specification = null)
        {
            return ApplySpecification(specification).Count();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Count(predicate);
        }

        public bool Any(ISpecification<TEntity> specification = null)
        {
            return ApplySpecification(specification).Any();
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Any(predicate);
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate, bool isAsNoTracking = false)
        {
            return isAsNoTracking
                ? _context.Set<TEntity>().AsNoTracking().Any(predicate)
                : _context.Set<TEntity>().Any(predicate);
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), spec);
        }

        public IEnumerable<TEntity> FindWithInclude(ISpecification<TEntity> specification, string includes, bool isAsNoTracking = false)
        {
            return isAsNoTracking ? ApplySpecification(specification).AsNoTracking().Include(includes) :
                ApplySpecification(specification).Include(includes);
        }

        public IEnumerable<TEntity> FindWithInclude(ISpecification<TEntity> specification, params string[] includes)
        {
            var query = ApplySpecification(specification);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

    }
}
