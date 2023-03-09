/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SeedService } from './seed.service';

describe('Service: Seed', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SeedService]
    });
  });

  it('should ...', inject([SeedService], (service: SeedService) => {
    expect(service).toBeTruthy();
  }));
});
