import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EquipmentService } from './application/equipment/equipment.service';
import { EquipmentComponent } from './presentation/equipment/equipment.component';
import { GetListEquipmentComponent } from './presentation/equipment/get-list-equipment/get-list-equipment.component';
import { EquipmentApi } from './infrastructure/equipment/equipment.api';
import { EquipmentRepository } from './domain/equipment/equipment.repository';
import { EquipmentRepositoryImpl } from './infrastructure/equipment/equipment.repository.impl';
import { EquipmentMapper } from './application/equipment/equipment.mapper';
import { GetEquipmentsUseCase } from './application/equipment/use-cases/get-equipments.use-case';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    // declare any components, directives, or pipes here
    EquipmentComponent,
    GetListEquipmentComponent
  ],
  providers: [
    // provide any services or dependencies here
    EquipmentMapper,
    EquipmentApi,
    { provide: EquipmentRepository, useClass: EquipmentRepositoryImpl },
    GetEquipmentsUseCase,
    EquipmentService
  ]
})
export class InventoryModule { }
