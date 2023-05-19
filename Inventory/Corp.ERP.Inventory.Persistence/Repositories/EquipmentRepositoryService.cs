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
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IList<Equipment>> GetAllAsync(Predicate<Equipment> predicate)
    {
        return await _inventoryContext.Equipments
            .Include(inc => inc.StorageUnit)
            .Include(inc => inc.UsedBy)
            .Where(w => predicate(w))
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Equipment> GetByIdAsync(Guid id)
    {
        return await _inventoryContext.Equipments
            .Include(inc => inc.StorageUnit)
            .Include(inc => inc.UsedBy)
            .Where(f => f.Id.ToString().Equals(id.ToString()))
            .FirstOrDefaultAsync();
    }

    public async Task<Equipment> GetFirstOrDefaultAsync(Predicate<Equipment> predicate, Equipment defaultValue)
    {
        return await _inventoryContext.Equipments
            .Include(inc => inc.StorageUnit)
            .Include(inc => inc.UsedBy)
            .AsNoTracking()
            .FirstOrDefaultAsync(w => predicate(w)) ?? defaultValue;
    }

    public async Task<int> UpdateAsync(Equipment entity)
    {
        _inventoryContext.Entry(entity).State = EntityState.Modified;
        //_inventoryContext.Equipments.UpdateAsync(entity);
        return await _inventoryContext.SaveChangesAsync();
    }

    public async Task<int> AddAsync(Equipment entity)
    {
        await _inventoryContext.Equipments.AddAsync(entity);
        return await _inventoryContext.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Equipment entity)
    {
        _inventoryContext.Equipments.Remove(entity);
        return await _inventoryContext.SaveChangesAsync();
    }
}
