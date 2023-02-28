/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { SeedComponent } from './seed.component';

describe('SeedComponent', () => {
  let component: SeedComponent;
  let fixture: ComponentFixture<SeedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SeedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SeedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
