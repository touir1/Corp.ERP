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
      id: storageUnit?.id,
      address: storageUnit?.address,
      name: storageUnit?.name
    };
  }

  mapToDomain(storageUnitDTO: StorageUnitDTO): StorageUnit {
    return {
      id: storageUnitDTO?.id,
      address: storageUnitDTO?.address,
      name: storageUnitDTO?.name
    };
  }
}
