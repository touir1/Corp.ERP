using Corp.ERP.Inventory.Application.Dto;
using Corp.ERP.Inventory.Application.Services;
using Corp.ERP.Inventory.Domain.Models;
using MediatR;

namespace Corp.ERP.Inventory.Application.Queries.GetEquipments;

public class GetEquipmentsQueryHandler : IRequestHandler<GetEquipmentsQuery, GetEquipmentsQueryResult>
{
    private IInventoryRepositoryService _inventoryRepositoryService;
    public GetEquipmentsQueryHandler(IInventoryRepositoryService repositoryService)
    {
        _inventoryRepositoryService = repositoryService;
    }
    public async Task<GetEquipmentsQueryResult> Handle(GetEquipmentsQuery request, CancellationToken cancellationToken)
    {
        return new GetEquipmentsQueryResult
        {
            Result = _inventoryRepositoryService.GetEquipments()
                .Select(s => (EquipmentDto) s).ToList(),
        };
    }
}
