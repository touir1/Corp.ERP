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
        return await _inventoryContext.Set<Equipment>()
            .Include(inc => inc.StorageUnit)
            .Include(inc => inc.UsedBy)
            .ToListAsync();
    }

    public Task<IList<Equipment>> GetAllAsync(Predicate<Equipment> predicate) => throw new NotImplementedException();

    public Task<Equipment> GetByIdAsync(Guid id) => throw new NotImplementedException();

    public Task<Equipment> GetFirstOrDefaultAsync(Predicate<Equipment> predicate, Equipment defaultValue) => throw new NotImplementedException();

    public Task UpdateAsync(Equipment entity) => throw new NotImplementedException();

    public Task AddAsync(Equipment entity) => throw new NotImplementedException();

    public Task DeleteAsync(Equipment entity) => throw new NotImplementedException();
}
