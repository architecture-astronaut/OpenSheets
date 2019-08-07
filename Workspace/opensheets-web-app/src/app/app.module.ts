import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { AlertDisplayComponent } from './components/alert-display/alert-display.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AlertDisplayComponent,
  ],
  imports: [
    BrowserModule,
	AppRoutingModule,
	FormsModule	,
	NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
