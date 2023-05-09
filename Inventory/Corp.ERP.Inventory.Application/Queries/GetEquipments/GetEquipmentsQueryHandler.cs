using MediatR;
using Corp.ERP.Inventory.Application.Contract.Repositories;
using Corp.ERP.Inventory.Application.Contract.Dto;

namespace Corp.ERP.Inventory.Application.Queries.GetEquipments;

public class GetEquipmentsQueryHandler : IRequestHandler<GetEquipmentsQuery, GetEquipmentsQueryResult>
{
    private IEquipmentRepositoryService _equipmentService;
    public GetEquipmentsQueryHandler(IEquipmentRepositoryService quipmentRepositoryService)
    {
        _equipmentService = quipmentRepositoryService;
    }
    public async Task<GetEquipmentsQueryResult> Handle(GetEquipmentsQuery request, CancellationToken cancellationToken)
    {
        var result = await _equipmentService.GetAllAsync();
        return new GetEquipmentsQueryResult
        {
            Equipments = result.Select(s => (EquipmentDto) s).ToList(),
        };
    }
}
