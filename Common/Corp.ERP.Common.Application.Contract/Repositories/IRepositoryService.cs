using Corp.ERP.Common.Domain.Contract.Models;
using System.Linq.Expressions;

namespace Corp.ERP.Common.Application.Repositories;

public interface IRepositoryService<T> where T : IEntity
{
    Task<IList<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
    Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, T defaultValue);
    Task<int> UpdateAsync(T entity);
    Task<int> DeleteAsync(T entity);
    Task<int> AddAsync(T entity);
}
