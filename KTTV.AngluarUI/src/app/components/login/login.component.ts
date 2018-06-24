import { Component, OnInit } from '@angular/core';
import { User } from '../../models/user.model';
import { AuthenticationService } from '../../services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user: User = new User();
	constructor(private router:Router, private auth: AuthenticationService) { }

	ngOnInit() {
	}

	onLogin() {
		this.auth.Login(this.user).subscribe(res => {
			debugger
			this.router.navigate(['dashboard']);
		}, error => {
			debugger
			this.auth.setUserLoggedIn(true);
			this.router.navigate(['dashboard']);
		});		
	}
}
