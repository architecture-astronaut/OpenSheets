import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { SpinnerOverlayComponent } from './components/spinner-overlay/spinner-overlay.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SpinnerOverlayComponent
  ],
  imports: [
    BrowserModule,
	AppRoutingModule,
	FormsModule	
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
