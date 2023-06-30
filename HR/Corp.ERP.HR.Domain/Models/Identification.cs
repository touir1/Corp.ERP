namespace Corp.ERP.HR.Domain.Models;

public class Identification : IEntity
{
    public Guid Id { get; set; }
    public virtual string UniqueIdentifier { get; set; }

    public virtual DateTime StartDate { get; set; }
    public virtual DateTime? ExpiryDate { get; set;}
    public virtual Guid IdentificationTypeId { get; set; }
    public virtual IdentificationType IdentificationType { get; set; }
    public virtual Guid OwnerId { get; set; }
    public virtual User Owner { get; set; }
}