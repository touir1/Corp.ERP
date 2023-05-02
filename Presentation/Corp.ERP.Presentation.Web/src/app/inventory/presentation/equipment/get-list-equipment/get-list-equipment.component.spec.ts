import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetListEquipmentComponent } from './get-list-equipment.component';

describe('GetListEquipmentComponent', () => {
  let component: GetListEquipmentComponent;
  let fixture: ComponentFixture<GetListEquipmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GetListEquipmentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetListEquipmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
