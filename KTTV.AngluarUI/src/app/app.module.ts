import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { BaseService } from './services/base.service';
import { AuthenticationService } from './services/authentication.service';
import { HttpClientModule } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { FormsModule } from '@angular/forms';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { RouterModule, Routes } from '@angular/router';
import {AuthguardGuard} from './guards/authguard.guard';
import { ListAmountRainComponent } from './components/list-amount-rain/list-amount-rain.component';
import { ChartAmountRainComponent } from './components/chart-amount-rain/chart-amount-rain.component';
import { LayoutComponent } from './components/layout/layout.component';

const appRoutes: Routes = [
	{
		path: '',
		component: LoginComponent
	},
	{
		path: 'dashboard',
		canActivate: [AuthguardGuard],
		component: DashboardComponent
	},	
	{
		path: 'list-amount-rain',
		canActivate: [AuthguardGuard],
		component: ListAmountRainComponent
	},
	{
		path: 'chart-amount-rain',
		canActivate: [AuthguardGuard],
		component: ChartAmountRainComponent
	}
]

@NgModule({
	declarations: [
		AppComponent,
		LoginComponent,
		DashboardComponent,
		ListAmountRainComponent,
		ChartAmountRainComponent,
		LayoutComponent
	],
	imports: [
		BrowserModule,
		HttpClientModule,
		FormsModule,
		RouterModule.forRoot(appRoutes),
	],
	providers: [
		BaseService,
		AuthenticationService,
		AuthguardGuard,
		CookieService],
	bootstrap: [AppComponent]
})
export class AppModule { }
