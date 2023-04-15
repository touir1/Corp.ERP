using Corp.ERP.Inventory.Domain.Models;

namespace Corp.ERP.Inventory.Application.ViewModel;

public class UserVM
{
    public virtual int Id { get; set; }
    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }
    public virtual string FullName { get; set; }
    public virtual string Email { get; set; }

    public static implicit operator UserVM(User _model)
    {
        return new UserVM
        {
            Id = _model.Id,
            FirstName = _model.FirstName,
            LastName = _model.LastName,
            FullName = _model.FullName,
            Email = _model.Email,
        };
    }
}