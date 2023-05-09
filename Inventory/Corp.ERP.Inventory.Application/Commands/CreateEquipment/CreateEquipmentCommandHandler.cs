using Corp.ERP.Inventory.Application.Contract.Dto;
using Corp.ERP.Inventory.Application.Contract.Repositories;
using Corp.ERP.Inventory.Application.Validators;
using Corp.ERP.Inventory.Domain.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Corp.ERP.Inventory.Application.Commands.CreateEquipment;

public class CreateEquipmentCommandHandler : IRequestHandler<CreateEquipmentCommand, CreateEquipmentCommandResult>
{
    private IEquipmentRepositoryService _equipmentService;

    public CreateEquipmentCommandHandler(IEquipmentRepositoryService _equipmentRepositoryService)
    {
        _equipmentService = _equipmentRepositoryService;
    }

    public async Task<CreateEquipmentCommandResult> Handle(CreateEquipmentCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateEquipmentCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var equipmentExists = await _equipmentService.GetFirstOrDefaultAsync(p => p.Code == request.Code, null);
        if (equipmentExists != null) {
            var message = $"An equipment with code {request.Code} already exists";
            throw new ValidationException(message, new[]
            {
                new ValidationFailure(nameof(request),message)
            });
        }
        //var storageExists = await _equipmentService.

        await _equipmentService.AddAsync((EquipmentDto) request);

        return  new CreateEquipmentCommandResult();
    }
}
