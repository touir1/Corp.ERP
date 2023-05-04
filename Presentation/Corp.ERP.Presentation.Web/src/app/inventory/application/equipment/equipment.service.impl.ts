import { forwardRef, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EquipmentService } from './base/equipment.service';
import { InventoryModule } from '../../inventory.module';
import { EquipmentDTO } from './DTOs/equipment.dto';
import { GetEquipmentsUseCase } from './use-cases/get-equipments.use-case';

@Injectable({
  providedIn: forwardRef(() => InventoryModule)
})
export class EquipmentServiceImpl implements EquipmentService {

  constructor(private getEquipmentsUseCase: GetEquipmentsUseCase) { }

  getAll(): Observable<EquipmentDTO[]> {
    return this.getEquipmentsUseCase.execute();
  }
}
