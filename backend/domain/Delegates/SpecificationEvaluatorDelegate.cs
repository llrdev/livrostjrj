using System.Linq;

namespace Domain.Delegates
{
    /// <summary>
    /// OnSpecificationOrderByEvaluating
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    public delegate void OnSpecificationOrderByEvaluating<TEntity>(ref IQueryable<TEntity> query);
}
