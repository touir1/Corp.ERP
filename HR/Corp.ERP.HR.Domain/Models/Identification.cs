namespace Corp.ERP.HR.Domain.Models;

public class Identification : IEntity
{
    public Guid Id { get; set; }
    public virtual string Code { get; set; }
    public virtual string Label { get; set; }
    public virtual string Description { get; set; }
    public virtual string IdNumber { get; set; }
    public virtual DateTime StartDate { get; set; }
    public virtual DateTime? ExpiryDate { get; set;}
}