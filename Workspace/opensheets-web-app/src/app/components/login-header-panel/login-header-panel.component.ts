import { Component, OnInit } from '@angular/core';
import { IdentityManagerService } from 'src/app/services/identity-manager/identity-manager.service';
import { Identity } from 'src/app/models/partial-identity';
import { Observable } from 'rxjs';

@Component({
  selector: 'opensheets-login-header-panel',
  templateUrl: './login-header-panel.component.html',
  styleUrls: ['./login-header-panel.component.scss']
})
export class LoginHeaderPanelComponent implements OnInit {
public identity$ : Observable<Identity>;
  constructor(private identityManager : IdentityManagerService) { }

  ngOnInit() {
	  this.identity$ = this.identityManager.getCurrentIdentity();
  }

}
