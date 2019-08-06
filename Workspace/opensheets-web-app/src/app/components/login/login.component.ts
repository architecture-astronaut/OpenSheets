import { Component, OnInit, NgModule } from '@angular/core';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'opensheets-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {

	username: string;
	password: string;

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  tryLogin(username: string, password: string): void {
	this.authService.login(username, password).then(() => {

	});
  }
}
