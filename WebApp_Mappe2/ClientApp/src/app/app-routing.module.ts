import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminpageComponent } from './adminpage/adminpage.component';
import { DestinasjonComponent } from './destinasjon/destinasjoner.component';
import { LoginComponent } from './login/login.component';
import { ListeRute } from './rute/liste/listeRute';

const appRoots: Routes = [
  { path: 'adminpage', component: AdminpageComponent },
  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'destinasjon', component: DestinasjonComponent },
  { path: 'rute', component: ListeRute }

]
@NgModule({
  imports: [
    RouterModule.forRoot(appRoots)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule {

}
