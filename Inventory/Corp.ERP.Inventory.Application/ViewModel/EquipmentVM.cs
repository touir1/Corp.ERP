using Corp.ERP.Inventory.Domain.Models;

namespace Corp.ERP.Inventory.Application.ViewModel;

public class EquipmentVM
{
    public virtual int Id { get; set; }
    public virtual string Name { get; set; }
    public virtual string Description { get; set; }
    public virtual string Code { get; set; }
    public virtual Boolean IsInUse { get; set; }
    public virtual DateTime? StartDateUsage { get; set; }
    public virtual UserVM? UsedBy { get; set; }
    public virtual StorageVM StorageUnit { get; set; }

    public static implicit operator EquipmentVM(Equipment _model)
    {
        return new EquipmentVM
        {
            Id = _model.Id,
            Name = _model.Name,
            Description = _model.Description,
            Code = _model.Code,
            IsInUse = _model.IsInUse,
            StorageUnit = _model.StorageUnit,
            StartDateUsage = _model.StartDateUsage,
            UsedBy = _model.UsedBy,
        };
    }
}
