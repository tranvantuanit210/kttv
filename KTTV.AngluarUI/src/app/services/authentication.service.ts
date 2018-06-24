import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { User } from '../models/user.model';
import { Observable } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class AuthenticationService extends BaseService {
	URL = 'api/authentication';
	private isUserLoggedIn;

	constructor(private httpClient: HttpClient, private cookie: CookieService) {
		super(httpClient, cookie);
		this.isUserLoggedIn = false;
	}

	Login(user: User): Observable<any> {
		return this.post(this.URL + '/login', user);
	}

	setUserLoggedIn(value) {
		this.isUserLoggedIn = value;
	}
  
	getUserLoggedIn() {
		return this.isUserLoggedIn;
	}
}