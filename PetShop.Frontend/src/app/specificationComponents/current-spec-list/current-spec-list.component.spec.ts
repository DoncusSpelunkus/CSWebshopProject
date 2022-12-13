import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CurrentSpecListComponent } from './current-spec-list.component';

describe('CurrentSpecListComponent', () => {
  let component: CurrentSpecListComponent;
  let fixture: ComponentFixture<CurrentSpecListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CurrentSpecListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CurrentSpecListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
