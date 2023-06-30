namespace Corp.ERP.HR.Domain.Models;

public class Title : IEntity
{
    public Guid Id { get; set; }
    public virtual string Code { get; set; }
    public virtual string Label { get; set; }
    public virtual string Description { get; set; }
}