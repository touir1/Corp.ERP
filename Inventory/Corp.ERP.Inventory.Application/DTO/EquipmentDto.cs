using Corp.ERP.Inventory.Domain.Models;

namespace Corp.ERP.Inventory.Application.Dto;

public class EquipmentDto
{
    public virtual int Id { get; set; }
    public virtual string Name { get; set; }
    public virtual string Description { get; set; }
    public virtual string Code { get; set; }
    public virtual Boolean IsInUse { get; set; }
    public virtual DateTime? StartDateUsage { get; set; }
    public virtual UserDto? UsedBy { get; set; }
    public virtual StorageDto StorageUnit { get; set; }

    public static implicit operator EquipmentDto(Equipment _model)
    {
        return new EquipmentDto
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
