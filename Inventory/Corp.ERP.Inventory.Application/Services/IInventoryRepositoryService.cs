using Corp.ERP.Inventory.Domain.Models;

namespace Corp.ERP.Inventory.Application.Services;

public interface IInventoryRepositoryService
{
    List<Equipment> GetEquipments();
}
