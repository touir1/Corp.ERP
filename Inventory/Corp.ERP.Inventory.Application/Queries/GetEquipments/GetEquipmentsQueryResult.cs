using Corp.ERP.Inventory.Application.Dto;

namespace Corp.ERP.Inventory.Application.Queries.GetEquipments;

public class GetEquipmentsQueryResult
{
    public IList<EquipmentDto> Equipments { get; set; }
}
