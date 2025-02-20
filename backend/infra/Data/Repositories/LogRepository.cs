using Domain.Core;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;

namespace Infra.Data.Repositories
{
    public class LogRepository<TEntity> : ILogRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext _context;
        static bool FlagVerbose = false;

        public LogRepository(DbContext context)
        {
            _context = context;
        }

        public async void Add(TEntity entity)
        {
            DumpLog(entity);
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        private void DumpLog(TEntity entity)
        {
            if (FlagVerbose)
            {
                string jsonString = JsonSerializer.Serialize(entity);
                Console.WriteLine(jsonString);
            }
        }

        public void SetVerbose()
        {
            FlagVerbose = true;
        }

    }
}
