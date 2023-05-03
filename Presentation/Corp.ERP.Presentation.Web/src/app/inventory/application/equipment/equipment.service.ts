import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { InventoryModule } from '../../inventory.module';
import { EquipmentDTO } from './DTOs/equipment.dto';
import { GetEquipmentsUseCase } from './use-cases/get-equipments.use-case';

@Injectable({
  providedIn: InventoryModule
})
export class EquipmentService {

  constructor(private getEquipmentsUseCase: GetEquipmentsUseCase) { }

  getAll(): Observable<EquipmentDTO[]> {
    return this.getEquipmentsUseCase.execute();
  }
}
