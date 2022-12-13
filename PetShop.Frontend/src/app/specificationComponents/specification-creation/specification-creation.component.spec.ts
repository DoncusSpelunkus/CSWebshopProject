import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SpecificationCreationComponent } from './specification-creation.component';

describe('SpecificationCreationComponent', () => {
  let component: SpecificationCreationComponent;
  let fixture: ComponentFixture<SpecificationCreationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SpecificationCreationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SpecificationCreationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
