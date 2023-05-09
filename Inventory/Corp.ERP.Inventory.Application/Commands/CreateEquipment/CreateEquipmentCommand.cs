using Corp.ERP.Inventory.Application.Contract.Dto;
using MediatR;

namespace Corp.ERP.Inventory.Application.Commands.CreateEquipment;

public class CreateEquipmentCommand : IRequest<CreateEquipmentCommandResult>
{
    public virtual string Name { get; set; }
    public virtual string Description { get; set; }
    public virtual string Code { get; set; }
    public virtual Boolean IsInUse { get; set; }
    public virtual DateTime? StartDateUsage { get; set; }
    public virtual Guid? UsedById { get; set; }
    public virtual Guid? StorageUnitId { get; set; }

    public static implicit operator EquipmentDto(CreateEquipmentCommand _model)
    {
        return new EquipmentDto
        {
            Name = _model.Name,
            Description = _model.Description,
            Code = _model.Code,
            IsInUse = _model.IsInUse,
            StartDateUsage = _model.StartDateUsage,
        };
    }

    public static implicit operator CreateEquipmentCommand(EquipmentDto _model)
    {
        return new CreateEquipmentCommand
        {
            Name = _model.Name,
            Description = _model.Description,
            Code = _model.Code,
            IsInUse = _model.IsInUse,
            StorageUnitId = _model.StorageUnit?.Id,
            StartDateUsage = _model.StartDateUsage,
            UsedById = _model.UsedBy?.Id,
        };
    }
}
