using Corp.ERP.Inventory.Application.Contract.Repositories;
using Corp.ERP.Inventory.Application.Dto;
using MediatR;

namespace Corp.ERP.Inventory.Application.Queries.GetEquipments;

public class GetEquipmentsQueryHandler : IRequestHandler<GetEquipmentsQuery, GetEquipmentsQueryResult>
{
    private IEquipmentRepositoryService _inventoryRepositoryService;
    public GetEquipmentsQueryHandler(IEquipmentRepositoryService repositoryService)
    {
        _inventoryRepositoryService = repositoryService;
    }
    public async Task<GetEquipmentsQueryResult> Handle(GetEquipmentsQuery request, CancellationToken cancellationToken)
    {
        return new GetEquipmentsQueryResult
        {
            Result = _inventoryRepositoryService.GetAll()
                .Select(s => (EquipmentDto) s).ToList(),
        };
    }
}
