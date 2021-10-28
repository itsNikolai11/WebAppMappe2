import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './login/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { Meny } from './meny/meny';
import { ListeRute } from './rute/liste/listeRute';
import { AdminpageComponent } from './adminpage/adminpage.component';
import { AppRoutingModule } from './app-routing.module';
import { DestinasjonComponent } from './destinasjon/destinasjonListe/destinasjoner.component';
import { AvgangComponent } from './avgang/avgang.component'
import { LagreRute } from './rute/lagre/lagreRute'; 

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    Meny,
    ListeRute,
    AdminpageComponent,
    DestinasjonComponent,
    LagreRute, 
    AvgangComponent 
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
