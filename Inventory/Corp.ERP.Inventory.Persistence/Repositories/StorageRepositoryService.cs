using Corp.ERP.Inventory.Application.Contract.Repositories;
using Corp.ERP.Inventory.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Corp.ERP.Inventory.Persistence.Repositories;

public class StorageRepositoryService : IStorageRepositoryService
{
    private readonly InventoryContext _inventoryContext;

    public StorageRepositoryService(InventoryContext inventoryContext)
    {
        _inventoryContext = inventoryContext;
    }

    public async Task<IList<Storage>> GetAllAsync()
    {
        return await _inventoryContext.Storages
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IList<Storage>> GetAllAsync(Expression<Func<Storage, bool>> predicate)
    {
        return await _inventoryContext.Storages
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
    }

    public async Task<Storage> GetByIdAsync(Guid id)
    {
        return await _inventoryContext.Storages
            .Where(f => f.Id.ToString().Equals(id.ToString()))
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<Storage> GetFirstOrDefaultAsync(Expression<Func<Storage, bool>> predicate, Storage defaultValue)
    {
        return await _inventoryContext.Storages
            .AsNoTracking()
            .FirstOrDefaultAsync(predicate) ?? defaultValue;
    }

    public async Task<int> UpdateAsync(Storage entity)
    {
        _inventoryContext.Entry(entity).State = EntityState.Modified;
        return await _inventoryContext.SaveChangesAsync();
    }

    public async Task<int> AddAsync(Storage entity)
    {
        await _inventoryContext.Storages.AddAsync(entity);
        return await _inventoryContext.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Storage entity)
    {
        _inventoryContext.Storages.Remove(entity);
        return await _inventoryContext.SaveChangesAsync();
    }
}
