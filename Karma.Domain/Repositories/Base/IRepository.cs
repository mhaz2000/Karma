using System.Linq.Expressions;

namespace Karma.Core.Repositories.Base
{
    public interface IRepository<TEntity> where TEntity : class
    {
        ValueTask<TEntity?> GetByIdAsync<Type>(Type id);
        IEnumerable<TEntity> AsEnumerable();
        Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicat);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> where);
        IEnumerable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeExpressions);
        bool Any(Expression<Func<TEntity, bool>> where);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where);
    }
}
