namespace Corp.ERP.Inventory.Domain.Models;

public class Equipment : InventoryItem, IEntity
{
    public virtual Storage StorageUnit { get; set; }
}
