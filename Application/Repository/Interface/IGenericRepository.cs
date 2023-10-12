
using Application.Common;

namespace Application.Repository
{
    public interface IGenericRepository<T> where T : class, new()
    {
        Task<int> SaveChangesAsync();
        Task<int> CreateAsync(T entity, bool isSaveChange = false);
        Task<int> CreateAsync(IEnumerable<T> entities, bool isSaveChange = false);
        Task<int> DeleteAsync(Expression<Func<T, bool>> filter);
        Task<int> UpdateAsync(T entity, bool isSaveChange = false);
        Task<int> UpdateAsync(Expression<Func<T, bool>>? predicate, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls);
        Task<T?> GetFirstOrDefault(QueryHelper<T> query, bool isAsNoTracking = true);
        Task<TResult?> GetFirstOrDefault<TResult>(QueryHelper<T, TResult> queryHelper, bool isAsNoTracking = true) where TResult : class;
        Task<IEnumerable<T>> Get(QueryHelper<T> queryHelper, bool isAsNoTracking = true);
        Task<IEnumerable<TResult>> Get<TResult>(QueryHelper<T, TResult> queryHelper, bool isAsNoTracking = true) where TResult : class;
        Task<PagedList<T>> GetWithPagination(QueryHelper<T> queryHelper, bool isAsNoTracking = true);
        Task<PagedList<TResult>> GetWithPagination<TResult>(QueryHelper<T, TResult> queryHelper, bool isAsNoTracking = true) where TResult : class;
    }
}
