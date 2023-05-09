using Corp.ERP.Common.Domain.Contract.Models;

namespace Corp.ERP.Common.Application.Repositories;

public interface IRepositoryService<T> where T : IEntity
{
    Task<IList<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task<IList<T>> GetAllAsync(Predicate<T> predicate);
    Task<T> GetFirstOrDefaultAsync(Predicate<T> predicate, T defaultValue);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task AddAsync(T entity);
}
