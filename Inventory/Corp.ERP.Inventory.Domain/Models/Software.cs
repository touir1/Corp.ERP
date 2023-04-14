namespace Corp.ERP.Inventory.Domain.Models;

internal class Software: InventoryItem
{
    public virtual DateTime ExpiryDate { get; set; }
}
