import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListAmountRainComponent } from './list-amount-rain.component';

describe('ListAmountRainComponent', () => {
  let component: ListAmountRainComponent;
  let fixture: ComponentFixture<ListAmountRainComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListAmountRainComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListAmountRainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
