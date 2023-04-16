using Corp.ERP.Inventory.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Corp.ERP.Inventory.Repository.Storage;

public interface IInventoryRepositoryService
{
    DbSet<Equipment> Equipments { get; set; }
}
