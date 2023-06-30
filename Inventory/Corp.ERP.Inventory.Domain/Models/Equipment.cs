namespace Corp.ERP.Inventory.Domain.Models;

public class Equipment : InventoryItem, IEntity
{
    public virtual Guid? StorageUnitId { get; set; }
    public virtual Storage StorageUnit { get; set; }
}
