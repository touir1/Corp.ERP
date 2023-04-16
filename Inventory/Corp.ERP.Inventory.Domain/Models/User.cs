using Corp.ERP.Common.Domain.Contract.Models;

namespace Corp.ERP.Inventory.Domain.Models;

public class User : IEntity
{
    public virtual int Id { get; set; }
    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }
    public string FullName
    {
        get { return FirstName + " " + LastName; }
    }
    public virtual string Email { get; set; }
}