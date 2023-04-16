using Corp.ERP.Inventory.Application.Contract.Repositories;
using Corp.ERP.Inventory.Domain.Models;
using Corp.ERP.Inventory.Persistence;

namespace Corp.ERP.Inventory.Storage.Repositories;

public class EquipmentRepositoryService : IEquipmentRepositoryService
{
    private readonly InventoryContext _inventoryContext;
    public EquipmentRepositoryService(InventoryContext inventoryContext)
    {
        _inventoryContext = inventoryContext;
    }

    public IList<Equipment> GetAll()
    {
        return _inventoryContext.Equipments.ToList();
    }

    public IList<Equipment> GetAll(Predicate<Equipment> predicate) => throw new NotImplementedException();

    public Equipment GetById(Guid id) => throw new NotImplementedException();

    public Equipment GetFirstOrDefault(Predicate<Equipment> predicate, Equipment defaultValue) => throw new NotImplementedException();

    public void Update(Equipment entity) => throw new NotImplementedException();

    public void Add(Equipment entity) => throw new NotImplementedException();

    public void Delete(Equipment entity) => throw new NotImplementedException();
}
