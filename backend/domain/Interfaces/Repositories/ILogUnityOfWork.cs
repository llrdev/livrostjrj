using Domain.Core;
using Domain.Delegates;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories

{
    public interface ILogUnityOfWork
    {
        Task<int> Complete();
        void Dispose();
        ILogRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        bool UseTransaction(OnUsingTransaction onUsingTransaction = null);
    }
}