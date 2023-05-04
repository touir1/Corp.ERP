import { Component, OnInit } from '@angular/core';
import { EquipmentService } from '../../../application/equipment/base/equipment.service';
import { EquipmentDTO } from '../../../application/equipment/DTOs/equipment.dto';

@Component({
  selector: 'app-get-list-equipment',
  templateUrl: './get-list-equipment.component.html',
  styleUrls: ['./get-list-equipment.component.css']
})
export class GetListEquipmentComponent implements OnInit {
  constructor(private service: EquipmentService) { }

  allEquipmentsSource: EquipmentDTO[] = [];
  displayedColumns: string[] = [];

  ngOnInit(): void {
    this.get();
  }

  get() {
    this.service.getAll().subscribe(data => {
      this.allEquipmentsSource = data;
    });
  }
}
