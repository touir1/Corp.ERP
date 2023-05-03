import { forwardRef, Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import { EquipmentRepository } from "../../../domain/equipment/equipment.repository";
import { InventoryModule } from "../../../inventory.module";
import { EquipmentDTO } from "../DTOs/equipment.dto";
import { EquipmentMapper } from "../equipment.mapper";

@Injectable({
  providedIn: forwardRef(() => InventoryModule)
})
export class GetEquipmentsUseCase {
  constructor(private repository: EquipmentRepository, private mapper: EquipmentMapper) { }

  execute(): Observable<EquipmentDTO[]> {
    return this.repository.getAll()
      .pipe(
        map(equipments => equipments.map(equipment => this.mapper.mapToDTO(equipment)))
      );
  }
}
