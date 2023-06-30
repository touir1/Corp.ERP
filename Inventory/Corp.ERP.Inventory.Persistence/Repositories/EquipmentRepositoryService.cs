using Corp.ERP.Inventory.Application.Contract.Repositories;
using Corp.ERP.Inventory.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

    public async Task<IList<Equipment>> GetAllAsync(Expression<Func<Equipment,bool>> predicate)
    {
        return await _inventoryContext.Equipments
            .Include(inc => inc.StorageUnit)
            .Include(inc => inc.UsedBy)
            .Where(predicate)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Equipment> GetByIdAsync(Guid id)
    {
        return await _inventoryContext.Equipments
            .Include(inc => inc.StorageUnit)
            .Include(inc => inc.UsedBy)
            .Where(f => f.Id.ToString().Equals(id.ToString()))
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<Equipment> GetFirstOrDefaultAsync(Expression<Func<Equipment,bool>> predicate, Equipment defaultValue)
    {
        return await _inventoryContext.Equipments
            .Include(inc => inc.StorageUnit)
            .Include(inc => inc.UsedBy)
            .AsNoTracking()
            .FirstOrDefaultAsync(predicate) ?? defaultValue;
    }

    public async Task<int> UpdateAsync(Equipment entity)
    {
        _inventoryContext.Entry(entity).State = EntityState.Modified;

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
