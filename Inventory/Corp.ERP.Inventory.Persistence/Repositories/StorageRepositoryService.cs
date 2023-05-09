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

    public async Task AddAsync(Storage entity)
    {
        await _inventoryContext.Set<Storage>().AddAsync(entity);
        await _inventoryContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Storage entity)
    {
        _inventoryContext.Set<Storage>().Remove(entity);
        await _inventoryContext.SaveChangesAsync();
    }

    public async Task<IList<Storage>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IList<Storage>> GetAllAsync(Predicate<Storage> predicate)
    {
        throw new NotImplementedException();
    }

    public async Task<Storage> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Storage> GetFirstOrDefaultAsync(Predicate<Storage> predicate, Storage defaultValue)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Storage entity)
    {
        throw new NotImplementedException();
    }
}
