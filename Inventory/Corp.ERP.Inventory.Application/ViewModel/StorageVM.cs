using Corp.ERP.Inventory.Domain.Models;

namespace Corp.ERP.Inventory.Application.ViewModel;

public class StorageVM
{
    public virtual int Id { get; set; }
    public virtual string Name { get; set; }
    public virtual string Address { get; set; }

    public static implicit operator StorageVM(Storage _model)
    {
        return new StorageVM
        {
            Id = _model.Id,
            Name = _model.Name,
            Address = _model.Address,
        };
    }
}