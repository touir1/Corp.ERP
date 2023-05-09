using Corp.ERP.Inventory.Application.Contract.Repositories;
using Corp.ERP.Inventory.Domain.Models;

namespace Corp.ERP.Inventory.Persistence.Repositories;

public class UserRepositoryService : IUserRepositoryService
{
    public Task AddAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<IList<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IList<User>> GetAllAsync(Predicate<User> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetFirstOrDefaultAsync(Predicate<User> predicate, User defaultValue)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }
}
