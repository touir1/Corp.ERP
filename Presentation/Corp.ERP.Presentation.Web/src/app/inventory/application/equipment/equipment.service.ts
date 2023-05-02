import { Injectable } from '@angular/core';
import { InventoryModule } from '../../inventory.module';

@Injectable({
  providedIn: InventoryModule
})
export class EquipmentService {

  constructor() { }
}
