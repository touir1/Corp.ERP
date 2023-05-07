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

    public IList<Equipment> GetAll()
    {
        return _inventoryContext.Set<Equipment>()
            .Include(inc => inc.StorageUnit)
            .Include(inc => inc.UsedBy)
            .ToList();
    }

    public IList<Equipment> GetAll(Predicate<Equipment> predicate) => throw new NotImplementedException();

    public Equipment GetById(Guid id) => throw new NotImplementedException();

    public Equipment GetFirstOrDefault(Predicate<Equipment> predicate, Equipment defaultValue) => throw new NotImplementedException();

    public void Update(Equipment entity) => throw new NotImplementedException();

    public void Add(Equipment entity) => throw new NotImplementedException();

    public void Delete(Equipment entity) => throw new NotImplementedException();
}
