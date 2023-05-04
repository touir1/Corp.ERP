import { forwardRef, Injectable } from '@angular/core';
import { Equipment } from '../../domain/equipment/equipment.entity';
import { InventoryModule } from '../../inventory.module';
import { UserMapper } from '../external/user.mapper';
import { StorageUnitMapper } from '../storage-unit/storage-unit.mapper';
import { EquipmentDTO } from './DTOs/equipment.dto';

@Injectable({
  providedIn: forwardRef(() => InventoryModule)
})
export class EquipmentMapper {

  constructor(private storageUnitMapper: StorageUnitMapper, private userMapper: UserMapper) { }

  mapToDTO(equipment: Equipment): EquipmentDTO {
    return {
      id: equipment.Id,
      code: equipment.Code,
      description: equipment.Description,
      isInUse: equipment.IsInUse,
      name: equipment.Name,
      startDateUsage: equipment.StartDateUsage,
      storageUnit: this.storageUnitMapper.mapToDTO(equipment.StorageUnit),
      usedBy: this.userMapper.mapToDTO(equipment.UsedBy)
    };
  }

  mapToDomain(equipmentDTO: EquipmentDTO): Equipment {
    return {
      Id: equipmentDTO.id,
      Code: equipmentDTO.code,
      Description: equipmentDTO.description,
      IsInUse: equipmentDTO.isInUse,
      Name: equipmentDTO.name,
      StartDateUsage: equipmentDTO.startDateUsage,
      StorageUnit: this.storageUnitMapper.mapToDomain(equipmentDTO.storageUnit),
      UsedBy: this.userMapper.mapToDomain(equipmentDTO.usedBy)
    };
  }

}
