namespace Corp.ERP.HR.Domain.Models;

public class User : IEntity
{
    public Guid Id { get; set; }
    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }
    public string FullName
    {
        get { return FirstName + " " + LastName; }
    }
    public virtual string Email { get; set; }
    public virtual DateTime BirthdayDate { get; set; }
    public virtual DateTime RecruitmentDate { get; set; }
    public virtual DateTime? LeavingDate { get; set; }
    public virtual double CurrentSalaryGross { get; set; }
    public virtual double CurrentSalaryNet { get; set; }
    public virtual Boolean IsDeleted { get; set; }
    public virtual List<Identification> Identifications { get; set; }
    public virtual Guid? ManagedById { get; set; }
    public virtual User? ManagedBy { get; set; }
    public virtual Guid CurrentTitleId { get; set; }
    public virtual Title CurrentTitle { get; set; }

}
