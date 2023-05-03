import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EquipmentService } from './application/equipment/equipment.service';
import { GetListEquipmentComponent } from './presentation/equipment/get-list-equipment/get-list-equipment.component';
//import { EquipmentComponent } from './presentation/equipment/equipment.component';
import { EquipmentApi } from './infrastructure/equipment/equipment.api';
import { EquipmentRepository } from './domain/equipment/equipment.repository';
import { EquipmentRepositoryImpl } from './infrastructure/equipment/equipment.repository.impl';
import { EquipmentMapper } from './application/equipment/equipment.mapper';
//import { GetListEquipmentsUseCase } from './application/equipment/use-cases/get-list-equipments.use-case';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    // declare any components, directives, or pipes here
    //EquipmentComponent,
    GetListEquipmentComponent
  ],
  providers: [
    // provide any services or dependencies here
    EquipmentApi,
    { provide: EquipmentRepository, useClass: EquipmentRepositoryImpl },
    EquipmentMapper,
    //GetListEquipmentsUseCase,
    EquipmentService
  ]
})
export class InventoryModule { }
