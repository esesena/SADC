/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PlantingService } from './planting.service';

describe('Service: Planting', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PlantingService]
    });
  });

  it('should ...', inject([PlantingService], (service: PlantingService) => {
    expect(service).toBeTruthy();
  }));
});
