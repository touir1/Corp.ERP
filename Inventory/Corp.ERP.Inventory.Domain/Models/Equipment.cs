namespace Corp.ERP.Inventory.Domain.Models;

internal class Equipment: InventoryItem
{
    public virtual Storage StorageUnit { get; set; }
}
