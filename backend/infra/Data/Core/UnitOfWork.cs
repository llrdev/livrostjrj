using Domain.Core;
using Domain.Delegates;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections;

namespace Infra.Data.Core
{
    /// <summary>
    /// Unit of Work
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SqlServerDbContext _context;
        private Hashtable _repositories;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(SqlServerDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="onUsingTransaction"></param>
        /// <returns></returns>
        public bool UseTransaction(OnUsingTransaction onUsingTransaction = null)
        {
            if (onUsingTransaction == null)
                return false;

            IDbContextTransaction transaction = null;
            using (transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    onUsingTransaction();
                    _context.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    if (transaction != null)
                        transaction.Rollback();
                    return false;
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Complete()
        {
            return _context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType
                        .MakeGenericType(typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<TEntity>)_repositories[type];
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }
    }


}