using Corp.ERP.Inventory.Application.Contract.Repositories;
using Corp.ERP.Inventory.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Corp.ERP.Inventory.Persistence.Repositories;

public class EquipmentRepositoryService : IEquipmentRepositoryService
{
    private readonly InventoryContext _inventoryContext;
    public EquipmentRepositoryService(InventoryContext inventoryContext)
    {
        _inventoryContext = inventoryContext;
    }

    public async Task<IList<Equipment>> GetAllAsync()
    {
        return await _inventoryContext.Equipments
            .Include(inc => inc.StorageUnit)
            .Include(inc => inc.UsedBy)
            .ToListAsync();
    }

    public async Task<IList<Equipment>> GetAllAsync(Predicate<Equipment> predicate)
    {
        return await _inventoryContext.Set<Equipment>()
            .Include(inc => inc.StorageUnit)
            .Include(inc => inc.UsedBy)
            .Where(w => predicate(w))
            .ToListAsync();
    }

    public async Task<Equipment> GetByIdAsync(Guid id)
    {
        return await _inventoryContext.Set<Equipment>()
            .Include(inc => inc.StorageUnit)
            .Include(inc => inc.UsedBy)
            .FirstAsync(w => w.Id == id);
    }

    public async Task<Equipment> GetFirstOrDefaultAsync(Predicate<Equipment> predicate, Equipment defaultValue)
    {
        return await _inventoryContext.Set<Equipment>()
            .Include(inc => inc.StorageUnit)
            .Include(inc => inc.UsedBy)
            .FirstAsync(w => predicate(w)) ?? defaultValue;
    }

    public async Task UpdateAsync(Equipment entity)
    {
        _inventoryContext.Entry(entity).State = EntityState.Modified;
        await _inventoryContext.SaveChangesAsync();
    }

    public async Task AddAsync(Equipment entity)
    {
        await _inventoryContext.Set<Equipment>().AddAsync(entity);
        await _inventoryContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Equipment entity)
    {
        _inventoryContext.Set<Equipment>().Remove(entity);
        await _inventoryContext.SaveChangesAsync();
    }
}
