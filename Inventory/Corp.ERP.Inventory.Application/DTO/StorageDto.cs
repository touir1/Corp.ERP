using Corp.ERP.Inventory.Domain.Models;

namespace Corp.ERP.Inventory.Application.Dto;

public class StorageDto
{
    public virtual int Id { get; set; }
    public virtual string Name { get; set; }
    public virtual string Address { get; set; }

    public static implicit operator StorageDto(Storage _model)
    {
        return new StorageDto
        {
            Id = _model.Id,
            Name = _model.Name,
            Address = _model.Address,
        };
    }
}