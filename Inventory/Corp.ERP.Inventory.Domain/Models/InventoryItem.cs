namespace Corp.ERP.Inventory.Domain.Models;

public abstract class InventoryItem
{
    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; }
    public virtual string Description { get; set; }
    public virtual string Code { get; set; }
    public virtual Boolean IsInUse { get; set; }
    public virtual DateTime? StartDateUsage { get; set; }
    public virtual Guid? UsedById { get; set; }
    public virtual User? UsedBy { get; set; }

}
