using Corp.ERP.Inventory.Application.Contract.Repositories;
using Corp.ERP.Inventory.Domain.Models;
using Corp.ERP.Inventory.Repository.Storage;

namespace Corp.ERP.Inventory.Storage.Repositories;

public class EquipmentRepositoryService : IEquipmentRepositoryService
{
    private readonly IInventoryRepositoryService _inventoryRepositoryService;
    public EquipmentRepositoryService(IInventoryRepositoryService inventoryRepositoryService)
    {
        _inventoryRepositoryService = inventoryRepositoryService;
    }

    public IList<Equipment> GetAll()
    {
        return _inventoryRepositoryService.Equipments.ToList();
    }

    public IList<Equipment> GetAllByPredicate(Predicate<Equipment> predicate) => throw new NotImplementedException();

    public Equipment GetById(int id) => throw new NotImplementedException();

    public Equipment GetFirstOrDefault(Predicate<Equipment> predicate, Equipment defaultValue) => throw new NotImplementedException();

    public void Update(Equipment entity) => throw new NotImplementedException();

    public void Add(Equipment entity) => throw new NotImplementedException();

    public void Delete(Equipment entity) => throw new NotImplementedException();
}
