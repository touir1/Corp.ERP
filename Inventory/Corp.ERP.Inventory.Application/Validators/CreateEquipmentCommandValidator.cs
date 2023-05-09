using Corp.ERP.Inventory.Application.Commands.CreateEquipment;
using FluentValidation;

namespace Corp.ERP.Inventory.Application.Validators;

public class CreateEquipmentCommandValidator: AbstractValidator<CreateEquipmentCommand>
{
    public CreateEquipmentCommandValidator()
    {
        RuleFor(x => x.Name).NotNull().MaximumLength(256);
        RuleFor(x => x.Description).MaximumLength(2000);
        RuleFor(x => x.Code).NotNull().MaximumLength(256);
        RuleFor(x => x.UsedById).Must(m => m == null || !Guid.Empty.Equals(m));
        RuleFor(x => x.StorageUnitId).Must(m => m == null || !Guid.Empty.Equals(m));
    }
}
