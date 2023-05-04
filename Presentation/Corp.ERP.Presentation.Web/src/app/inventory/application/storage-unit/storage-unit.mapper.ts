import { forwardRef, Injectable } from "@angular/core";
import { StorageUnit } from "../../domain/storage-unit/storage-unit.entity";
import { InventoryModule } from "../../inventory.module";
import { StorageUnitDTO } from "../equipment/DTOs/storage-unit.dto";

@Injectable({
  providedIn: forwardRef(() => InventoryModule)
})
export class StorageUnitMapper {
  mapToDTO(storageUnit: StorageUnit): StorageUnitDTO {
    return {
      id: storageUnit.Id,
      address: storageUnit.Address,
      name: storageUnit.Name
    };
  }

  mapToDomain(storageUnitDTO: StorageUnitDTO): StorageUnit {
    return {
      Id: storageUnitDTO.id,
      Address: storageUnitDTO.address,
      Name: storageUnitDTO.name
    };
  }
}
