namespace Corp.ERP.Inventory.Domain.Models;

public class Storage : IEntity
{
    public virtual int Id { get; set; }
    public virtual string Name { get; set; }
    public virtual string Address { get; set; }

}
