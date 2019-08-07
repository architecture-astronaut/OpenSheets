import { Component, OnInit } from '@angular/core';
import { AlertService } from 'src/app/services/alert/alert.service';
import { Alert } from 'src/app/common/alert';
import { Observable } from 'rxjs';

@Component({
  selector: 'opensheets-alert-display',
  templateUrl: './alert-display.component.html',
  styleUrls: ['./alert-display.component.scss']
})
export class AlertDisplayComponent implements OnInit {
  alerts: Observable<Alert[]>;

  constructor(private alertService: AlertService) { }

  ngOnInit() {
	  this.alerts = this.alertService.get();
  }

  close(alert: Alert) {

  }
}
