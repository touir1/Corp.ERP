import { forwardRef, Injectable } from '@angular/core';
import { Equipment } from '../../domain/equipment/equipment.entity';
import { InventoryModule } from '../../inventory.module';
import { UserMapper } from '../user/user.mapper';
import { StorageUnitMapper } from '../storage-unit/storage-unit.mapper';
import { EquipmentDTO } from './DTOs/equipment.dto';

@Injectable({
  providedIn: forwardRef(() => InventoryModule)
})
export class EquipmentMapper {

  constructor(private storageUnitMapper: StorageUnitMapper, private userMapper: UserMapper) { }

  mapToDTO(equipment: Equipment): EquipmentDTO {
    return {
      id: equipment.id,
      code: equipment.code,
      description: equipment.description,
      isInUse: equipment.isInUse,
      name: equipment.name,
      startDateUsage: equipment.startDateUsage,
      storageUnit: this.storageUnitMapper.mapToDTO(equipment.storageUnit),
      usedBy: this.userMapper.mapToDTO(equipment.usedBy)
    };
  }

  mapToDomain(equipmentDTO: EquipmentDTO): Equipment {
    return {
      id: equipmentDTO.id,
      code: equipmentDTO.code,
      description: equipmentDTO.description,
      isInUse: equipmentDTO.isInUse,
      name: equipmentDTO.name,
      startDateUsage: equipmentDTO.startDateUsage,
      storageUnit: this.storageUnitMapper.mapToDomain(equipmentDTO.storageUnit),
      usedBy: this.userMapper.mapToDomain(equipmentDTO.usedBy)
    };
  }

}
