import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-list-amount-rain',
  templateUrl: './list-amount-rain.component.html',
  styleUrls: ['./list-amount-rain.component.css']
})
export class ListAmountRainComponent implements OnInit {

  constructor(private auth: AuthenticationService) { }

  ngOnInit() {
  }

}
