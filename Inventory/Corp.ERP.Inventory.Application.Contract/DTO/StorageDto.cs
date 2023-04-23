using Corp.ERP.Inventory.Domain.Models;

namespace Corp.ERP.Inventory.Application.Contract.Dto;

public class StorageDto
{
    public virtual Guid Id { get; set; }
    public virtual string Name { get; set; }
    public virtual string Address { get; set; }

    public static implicit operator StorageDto(Storage _model)
    {
        if (_model == null) 
            return null;

        return new StorageDto
        {
            Id = _model.Id,
            Name = _model.Name,
            Address = _model.Address,
        };
    }
}