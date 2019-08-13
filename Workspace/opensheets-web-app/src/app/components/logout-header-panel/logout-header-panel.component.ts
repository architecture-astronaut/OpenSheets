import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { LoginModalComponent } from '../modals/login-modal/login-modal.component';

@Component({
  selector: 'opensheets-logout-header-panel',
  templateUrl: './logout-header-panel.component.html',
  styleUrls: ['./logout-header-panel.component.scss']
})
export class LogoutHeaderPanelComponent implements OnInit {

  constructor(private modalService: NgbModal) { }

  ngOnInit() {
  }

  openSignIn() {
	const modalRef = this.modalService.open(LoginModalComponent, { centered: true, backdropClass: 'stripe-backdrop'});
  }
}
