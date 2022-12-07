import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SpecificationUpdateComponent } from './specification-update.component';

describe('SpecificationUpdateComponent', () => {
  let component: SpecificationUpdateComponent;
  let fixture: ComponentFixture<SpecificationUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SpecificationUpdateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SpecificationUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
