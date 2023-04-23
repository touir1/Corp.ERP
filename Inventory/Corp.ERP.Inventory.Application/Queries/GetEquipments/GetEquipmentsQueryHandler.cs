using MediatR;
using Corp.ERP.Inventory.Application.Contract.Repositories;
using Corp.ERP.Inventory.Application.Contract.Dto;

namespace Corp.ERP.Inventory.Application.Queries.GetEquipments;

public class GetEquipmentsQueryHandler : IRequestHandler<GetEquipmentsQuery, GetEquipmentsQueryResult>
{
    private IEquipmentRepositoryService _inventoryRepositoryService;
    public GetEquipmentsQueryHandler(IEquipmentRepositoryService repositoryService)
    {
        _inventoryRepositoryService = repositoryService;
    }
    public Task<GetEquipmentsQueryResult> Handle(GetEquipmentsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new GetEquipmentsQueryResult
        {
            Equipments = _inventoryRepositoryService.GetAll()
                .Select(s => (EquipmentDto) s).ToList(),
        });
    }
}
