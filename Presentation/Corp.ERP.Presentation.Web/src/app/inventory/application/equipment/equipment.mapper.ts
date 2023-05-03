import { Injectable } from '@angular/core';
import { Equipment } from '../../domain/equipment/equipment.entity';
import { InventoryModule } from '../../inventory.module';
import { EquipmentDTO } from './DTOs/equipment.dto';

@Injectable({
  providedIn: InventoryModule
})
export class EquipmentMapper {

  mapToDTO(equipment: Equipment): EquipmentDTO {
    return {
      //id: equipment.id,
    };
  }

  mapToDomain(equipmentDTO: EquipmentDTO): Equipment {
    return {
      //id: equipmentDTO.id,
    };
  }

}
