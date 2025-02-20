using Domain.Core;
using Domain.Delegates;
using Domain.Interfaces.Repositories;
using System;

namespace Domain.Interfaces
{
    /// <summary>
    /// Unidade de trabalho
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        int Complete();

        bool UseTransaction(OnUsingTransaction onUsingTransaction = null);

    }


}