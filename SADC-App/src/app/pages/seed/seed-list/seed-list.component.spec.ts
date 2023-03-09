/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { SeedListComponent } from './seed-list.component';

describe('SeedListComponent', () => {
  let component: SeedListComponent;
  let fixture: ComponentFixture<SeedListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SeedListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SeedListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
