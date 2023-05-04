import { TestBed } from '@angular/core/testing';

import { EquipmentServiceImpl } from './equipment.service.impl';

describe('EquipmentService', () => {
  let service: EquipmentServiceImpl;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EquipmentServiceImpl);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
