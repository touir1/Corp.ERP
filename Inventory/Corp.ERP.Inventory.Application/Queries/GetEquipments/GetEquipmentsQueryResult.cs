using Corp.ERP.Inventory.Application.Contract.Dto;

namespace Corp.ERP.Inventory.Application.Queries.GetEquipments;

public class GetEquipmentsQueryResult
{
    public IList<EquipmentDto> Equipments { get; set; }
}
