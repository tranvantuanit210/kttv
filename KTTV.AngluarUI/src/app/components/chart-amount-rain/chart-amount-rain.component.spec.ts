import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChartAmountRainComponent } from './chart-amount-rain.component';

describe('ChartAmountRainComponent', () => {
  let component: ChartAmountRainComponent;
  let fixture: ComponentFixture<ChartAmountRainComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChartAmountRainComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChartAmountRainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
