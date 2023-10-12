

using Application.Common;

namespace Application.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        protected readonly DbContext dbContext;
        protected DbSet<T> dbSet;
        public GenericRepository(DbContext context)
        {
            dbContext = context;
            dbSet = context.Set<T>();
        }
        public async Task<int> CreateAsync(T entity, bool isSaveChange = false)
        {
            await dbSet.AddAsync(entity);

            if (isSaveChange)
            {
                return await SaveChangesAsync().ConfigureAwait(false);
            }
            return 0;
        }
        public async Task<int> CreateAsync(IEnumerable<T> entities, bool isSaveChange = false)
        {
            List<T> values = entities.ToList();

            await dbSet.AddRangeAsync(values);

            if (isSaveChange)
            {
                return await SaveChangesAsync().ConfigureAwait(false);
            }
            return 0;
        }
        public async Task<int> DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate)
                                .ExecuteDeleteAsync()
                                .ConfigureAwait(false);
        }
        public async Task<int> UpdateAsync(T entity, bool isSaveChange = false)
        {
            dbContext.Attach(entity).State = EntityState.Modified;
            if (isSaveChange)
            {
                return await SaveChangesAsync();
            }
            return 0;
        }
        public async Task<int> UpdateAsync(Expression<Func<T, bool>>? predicate, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls)
        {
            var query = dbSet.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ExecuteUpdateAsync(setPropertyCalls);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }
        public async Task<T?> GetFirstOrDefault(QueryHelper<T> queryHelper, bool isAsNoTracking = true)
        {
            if (queryHelper == null)
            {
                queryHelper = new QueryHelper<T>();
            }
            var query = dbSet.ApplyConditions(queryHelper, isAsNoTracking);

            return await query.SingleOrDefaultAsync().ConfigureAwait(false);
        }
        public async Task<TResult?> GetFirstOrDefault<TResult>(QueryHelper<T, TResult> queryHelper, bool isAsNoTracking = true) where TResult : class
        {
            if (queryHelper == null)
            {
                queryHelper = new QueryHelper<T, TResult>();
            }
            var query = dbSet.ApplyConditions<T, TResult>(queryHelper, isAsNoTracking);

            return await query.SingleOrDefaultAsync().ConfigureAwait(false);
        }
        public async Task<IEnumerable<T>> Get(QueryHelper<T> queryHelper, bool isAsNoTracking = true)
        {
            if (queryHelper == null)
            {
                queryHelper = new QueryHelper<T>();
            }
            var query = dbSet.ApplyConditions(queryHelper, isAsNoTracking: isAsNoTracking);

            return await query.ToListAsync().ConfigureAwait(false);
        }
        public async Task<IEnumerable<TResult>> Get<TResult>(QueryHelper<T, TResult> queryHelper, bool isAsNoTracking = true) where TResult : class
        {
            if (queryHelper == null)
            {
                queryHelper = new QueryHelper<T, TResult>();
            }
            var query = dbSet.ApplyConditions(queryHelper, isAsNoTracking: isAsNoTracking);

            return await query.ToListAsync().ConfigureAwait(false);
        }
        public async Task<PagedList<T>> GetWithPagination(QueryHelper<T> queryHelper, bool isAsNoTracking = true)
        {
            if (queryHelper == null)
            {
                queryHelper = new QueryHelper<T>();
            }

            var pagedList = new PagedList<T>();

            var query = dbSet.ApplyConditions(queryHelper, isAsNoTracking: isAsNoTracking);

            await pagedList.LoadData(query, queryHelper.PaginationParams).ConfigureAwait(false);

            return pagedList;
        }
        public async Task<PagedList<TResult>> GetWithPagination<TResult>(QueryHelper<T, TResult> queryHelper, bool isAsNoTracking = true) where TResult : class
        {
            if (queryHelper == null)
            {
                queryHelper = new();
            }

            var pagedList = new PagedList<TResult>();
            if (queryHelper == null)
            {
                queryHelper = new QueryHelper<T, TResult>();
            }

            var query = dbSet.ApplyConditions(queryHelper, isAsNoTracking: isAsNoTracking);

            await pagedList.LoadData(query, queryHelper.PaginationParams).ConfigureAwait(false);

            return pagedList;
        }

    }
}
