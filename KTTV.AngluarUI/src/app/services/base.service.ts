import { Injectable } from '@angular/core';

import { environment } from '../../environments/environment';
import { CookieService } from 'ngx-cookie-service';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable()
export class BaseService {
	private baseURL = environment.baseUrl;

	private static handleError(error: HttpErrorResponse) {
		const errMsg = (error.message) ? error.message :
			error.status ? `${error.status} - ${error.statusText}` : 'Server error';
		// console.error(errMsg);
		return Observable.throw(errMsg);
	}

	constructor(private http: HttpClient, private cookieService: CookieService) { }

	get headers(): HttpHeaders {
		return new HttpHeaders({
			'Content-Type': 'application/json',
			'Accept': 'q=0.8;application/json;q=0.9',
			'APIKey': '~123456789~',
			'Authorization': 'Bearer ' + this.cookieService.get('AccessToken')
		});
	}

	get options(): any {
		return {
			headers: this.headers
		};
	}

	get<T>(url: string): Observable<T> {
		return this.http
			.get(`${this.baseURL}/${url}`, this.options)
			.pipe(
				map((res) => this.extractData<T>(res)),
				catchError(BaseService.handleError)
			);
	}

	getWithDynamicQueryTerm<T>(url: string, key: string, val: string): Observable<T> {
		return this.http
			.get(`${this.baseURL}/${url}/?${key}=${val}`, this.options)
			.pipe(
				map((res) => this.extractData<T>(res)),
				catchError(BaseService.handleError)
			);
	}

	getWithFixedQueryString<T>(url: string, param: any): Observable<T> {
		const params = new HttpParams().append('query', param);
		const options = { headers: this.headers, params: params };
		return this.http
			.get(`${this.baseURL}/${url}`, options)
			.pipe(
				map((res) => this.extractData<T>(res)),
				catchError(BaseService.handleError)
			);
	}

	getWithObjectAsQueryString<T>(url: string, param: any): Observable<T> {
		const params: HttpParams = new HttpParams();
		for (const key in param) {
			if (param.hasOwnProperty(key)) {
				const val = param[key];
				params.set(key, val);
			}
		}
		const options = { headers: this.headers, params: params };
		return this.http
			.get(`${this.baseURL}/${url}`, options)
			.pipe(
				map((res) => this.extractData<T>(res)),
				catchError(BaseService.handleError)
			);
	}

	post<T>(url: string, param: any): Observable<T> {
		const body = JSON.stringify(param);
		return this.http
			.post(`${this.baseURL}/${url}`, body, this.options)
			.pipe(
				map((res) => this.extractData<T>(res)),
				catchError(BaseService.handleError)
			);
	}

	update<T>(url: string, param: any): Observable<T> {
		const body = JSON.stringify(param);
		return this.http
			.put(`${this.baseURL}/${url}`, body, this.options)
			.pipe(
				map((res) => this.extractData<T>(res)),
				catchError(BaseService.handleError)
			);
	}

	patch<T>(url: string, param: any): Observable<T> {
		const body = JSON.stringify(param);
		return this.http
			.patch(`${this.baseURL}/${url}`, body, this.options)
			.pipe(
				map((res) => this.extractData<T>(res)),
				catchError(BaseService.handleError)
			);
	}

	delete<T>(url: string, param: any): Observable<T> {
		const params: HttpParams = new HttpParams();
		for (const key in param) {
			if (param.hasOwnProperty(key)) {
				const val = param[key];
				params.set(key, val);
			}
		}
		const options = { headers: this.headers, params: params };
		return this.http
			.delete(`${this.baseURL}/${url}`, options)
			.pipe(
				map((res) => this.extractData<T>(res)),
				catchError(BaseService.handleError)
			);
	}

	deleteWithKey<T>(url: string, key: string, val: string): Observable<T> {
		return this.http
			.delete(`${this.baseURL}/${url}/?${key}=${val}`, this.options)
			.pipe(
				map((res) => this.extractData<T>(res)),
				catchError(BaseService.handleError)
			);
	}

	private extractData<T>(res: any): T {
		if (res.status < 200 || res.status >= 300) {
			throw new Error('Bad response status: ' + res.status);
		}
		const body = res as T;
		return body;
	}
}