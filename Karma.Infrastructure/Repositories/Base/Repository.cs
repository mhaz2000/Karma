using Karma.Core.Repositories.Base;
using Karma.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Karma.Infrastructure.Repositories.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DataContext Context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(DataContext context)
        {
            this.Context = context;
            _dbSet = Context.Set<TEntity>();
        }
        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
        }

        public async Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public IEnumerable<TEntity> AsEnumerable()
        {
            return Context.Set<TEntity>().AsEnumerable();
        }

        public ValueTask<TEntity?> GetByIdAsync<Type>(Type id)
        {
            return Context.Set<TEntity>().FindAsync(id);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> where)
        {
            IQueryable<TEntity> dbQuery = Context.Set<TEntity>();
            var query = dbQuery.Where(where);
            return query;
        }

        public IEnumerable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            return includeExpressions.Aggregate(_dbSet.AsQueryable(), (query, path) => query.Include(path));
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> where)
        {
            IQueryable<TEntity> dbQuery = Context.Set<TEntity>();

            if (where != null)
                return dbQuery.Any(where);

            return dbQuery.Any();
        }

        public async virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where)
        {
            IQueryable<TEntity> dbQuery = Context.Set<TEntity>();

            if (where != null)
                return await dbQuery.AnyAsync(where);

            return await dbQuery.AnyAsync();
        }

    }
}
