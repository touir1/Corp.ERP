using Corp.ERP.Common.Domain.Contract.Models;

namespace Corp.ERP.Inventory.Domain.Models;

public class Equipment : InventoryItem, IEntity
{
    public virtual Storage StorageUnit { get; set; }
}
