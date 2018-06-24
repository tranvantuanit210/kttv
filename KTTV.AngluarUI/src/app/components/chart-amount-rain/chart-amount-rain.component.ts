import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
@Component({
  selector: 'app-chart-amount-rain',
  templateUrl: './chart-amount-rain.component.html',
  styleUrls: ['./chart-amount-rain.component.css']
})
export class ChartAmountRainComponent implements OnInit {

  constructor(private auth: AuthenticationService) { }

  ngOnInit() {
  }

}
