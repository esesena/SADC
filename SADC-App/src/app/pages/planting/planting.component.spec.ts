/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PlantingComponent } from './planting.component';

describe('PlantingComponent', () => {
  let component: PlantingComponent;
  let fixture: ComponentFixture<PlantingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlantingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlantingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
