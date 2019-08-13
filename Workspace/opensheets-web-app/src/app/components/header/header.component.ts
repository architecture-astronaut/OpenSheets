import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth/auth.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'opensheets-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  public loggedIn : Observable<boolean>;
  constructor(private authService: AuthService) { }

  ngOnInit() {
	  this.loggedIn = this.authService.watch();
  }

}
