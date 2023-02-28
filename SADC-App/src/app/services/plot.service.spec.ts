/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PlotService } from './plot.service';

describe('Service: Plot', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PlotService]
    });
  });

  it('should ...', inject([PlotService], (service: PlotService) => {
    expect(service).toBeTruthy();
  }));
});
