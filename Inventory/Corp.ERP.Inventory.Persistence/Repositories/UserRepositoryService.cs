using Corp.ERP.Inventory.Application.Contract.Repositories;
using Corp.ERP.Inventory.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Corp.ERP.Inventory.Persistence.Repositories;

public class UserRepositoryService : IUserRepositoryService
{
    private readonly InventoryContext _inventoryContext;

    public UserRepositoryService(InventoryContext inventoryContext)
    {
        _inventoryContext = inventoryContext;
    }

    public async Task<IList<User>> GetAllAsync()
    {
        return await _inventoryContext.Users
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IList<User>> GetAllAsync(Expression<Func<User, bool>> predicate)
    {
        return await _inventoryContext.Users
            .Where(predicate)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        return await _inventoryContext.Users
            .AsNoTracking()
            .Where(f => f.Id.ToString().Equals(id.ToString()))
            .FirstOrDefaultAsync();
    }

    public async Task<User> GetFirstOrDefaultAsync(Expression<Func<User, bool>> predicate, User defaultValue)
    {
        return await _inventoryContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(predicate) ?? defaultValue;
    }

    public async Task<int> UpdateAsync(User entity)
    {
        _inventoryContext.Entry(entity).State = EntityState.Modified;
        return await _inventoryContext.SaveChangesAsync();
    }

    public async Task<int> AddAsync(User entity)
    {
        await _inventoryContext.Users.AddAsync(entity);
        return await _inventoryContext.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(User entity)
    {
        _inventoryContext.Users.Remove(entity);
        return await _inventoryContext.SaveChangesAsync();
    }
}
