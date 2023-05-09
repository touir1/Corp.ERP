using Corp.ERP.Inventory.Application.Contract.Repositories;
using Corp.ERP.Inventory.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Corp.ERP.Inventory.Persistence.Repositories;

internal class StorageRepositoryService : IStorageRepositoryService
{
    private readonly InventoryContext _inventoryContext;

    public StorageRepositoryService(InventoryContext inventoryContext)
    {
        _inventoryContext = inventoryContext;
    }

    public async Task<IList<Storage>> GetAllAsync()
    {
        return await _inventoryContext.Storages
            .ToListAsync();
    }

    public async Task<IList<Storage>> GetAllAsync(Predicate<Storage> predicate)
    {
        return await _inventoryContext.Storages
            .Where(w => predicate(w))
            .ToListAsync();
    }

    public async Task<Storage> GetByIdAsync(Guid id)
    {
        return await _inventoryContext.Storages
            .FirstAsync(w => w.Id == id);
    }

    public async Task<Storage> GetFirstOrDefaultAsync(Predicate<Storage> predicate, Storage defaultValue)
    {
        return await _inventoryContext.Storages
            .FirstAsync(w => predicate(w)) ?? defaultValue;
    }

    public async Task UpdateAsync(Storage entity)
    {
        _inventoryContext.Entry(entity).State = EntityState.Modified;
        await _inventoryContext.SaveChangesAsync();
    }

    public async Task AddAsync(Storage entity)
    {
        await _inventoryContext.Storages.AddAsync(entity);
        await _inventoryContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Storage entity)
    {
        _inventoryContext.Storages.Remove(entity);
        await _inventoryContext.SaveChangesAsync();
    }
}
