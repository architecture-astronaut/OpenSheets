import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IdentityService } from 'src/app/services/identity/identity.service';
import { Identity } from 'src/app/models/identity';

@Component({
  selector: 'opensheets-login-header-panel',
  templateUrl: './login-header-panel.component.html',
  styleUrls: ['./login-header-panel.component.scss']
})
export class LoginHeaderPanelComponent implements OnInit {
public identity$ : Observable<Identity>;
  constructor(private identityService : IdentityService) { }

  ngOnInit() {
	  this.identity$ = this.identityService.getCurrentIdentity();
  }

}
