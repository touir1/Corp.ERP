using Corp.ERP.Inventory.Application.Contract.Repositories;
using Corp.ERP.Inventory.Domain.Models;
using Microsoft.EntityFrameworkCore;

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
            .ToListAsync();
    }

    public async Task<IList<User>> GetAllAsync(Predicate<User> predicate)
    {
        return await _inventoryContext.Users
            .Where(w => predicate(w))
            .ToListAsync();
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        return await _inventoryContext.Users
            .FirstAsync(w => w.Id == id);
    }

    public async Task<User> GetFirstOrDefaultAsync(Predicate<User> predicate, User defaultValue)
    {
        return await _inventoryContext.Users
            .FirstAsync(w => predicate(w)) ?? defaultValue;
    }

    public async Task UpdateAsync(User entity)
    {
        _inventoryContext.Entry(entity).State = EntityState.Modified;
        await _inventoryContext.SaveChangesAsync();
    }

    public async Task AddAsync(User entity)
    {
        await _inventoryContext.Users.AddAsync(entity);
        await _inventoryContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(User entity)
    {
        _inventoryContext.Users.Remove(entity);
        await _inventoryContext.SaveChangesAsync();
    }
}
