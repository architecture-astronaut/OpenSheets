import { Component, OnInit, NgModule } from '@angular/core';
import { AuthService } from 'src/app/services/auth/auth.service';
import { SpinnerOverlayService } from 'src/app/services/spinner-overlay/spinner-overlay.service';

@Component({
  selector: 'opensheets-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {

	username: string;
	password: string;

  constructor(private authService: AuthService, private spinnerService: SpinnerOverlayService) { }

  ngOnInit() {
  }

  tryLogin(username: string, password: string): void {
	this.spinnerService.show("Signing in!")
	this.authService.login(username, password).then(() => {
		this.spinnerService.hide();
	},
	() => {
		this.spinnerService.hide();
	});
  }

  openForgotPassword() : void {
	  
  }
}
