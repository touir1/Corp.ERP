using Corp.ERP.Inventory.Application.ViewModel;

namespace Corp.ERP.Inventory.Application.Queries.GetEquipments;

public class GetEquipmentsQueryResult
{
    IList<EquipmentVM> Result { get; set; }
}
