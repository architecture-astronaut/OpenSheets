import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { FontAwesomeModule, FaIconLibrary } from '@fortawesome/angular-fontawesome';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { AlertDisplayComponent } from './components/alert-display/alert-display.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';

import { fas } from '@fortawesome/free-solid-svg-icons';
import { far } from '@fortawesome/free-regular-svg-icons';
import { LogoutHeaderPanelComponent } from './components/logout-header-panel/logout-header-panel.component';
import { LoginHeaderPanelComponent } from './components/login-header-panel/login-header-panel.component';
import { StorageServiceModule } from 'angular-webstorage-service';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './components/pages/home/home.component';
import { LoginModalComponent } from './components/modals/login-modal/login-modal.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AlertDisplayComponent,
    HeaderComponent,
    FooterComponent,
    LogoutHeaderPanelComponent,
    LoginHeaderPanelComponent,
    HomeComponent,
    LoginModalComponent,
  ],
  imports: [
    BrowserModule,
	AppRoutingModule,
	FormsModule	,
	NgbModule,
	FontAwesomeModule,
	StorageServiceModule ,
	HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [LoginModalComponent]
})
export class AppModule {
	constructor(library: FaIconLibrary){
		library.addIconPacks(fas, far)
	}
 }
