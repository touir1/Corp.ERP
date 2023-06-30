namespace Corp.ERP.HR.Domain.Models;

public class IdentificationType : IEntity
{
    public Guid Id { get; set; }
    public virtual string Code { get; set; }
    public virtual String Label { get; set; }
    public virtual String Description { get; set; }
}
