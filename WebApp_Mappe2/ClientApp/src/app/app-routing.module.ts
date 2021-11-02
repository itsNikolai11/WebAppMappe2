import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router'; 
import { AdminpageComponent } from './adminpage/adminpage.component';
import { LoginComponent } from './login/login.component';
import { ListeRute } from './rute/liste/listeRute';
import { AvgangComponent } from './avgang/avgangListe/avgang.component';
import { DestinasjonComponent } from './destinasjon/destinasjonListe/destinasjoner.component';
import { DestinasjonLagre } from './destinasjon/destinasjonLagre/destinasjonLagre';
import { DestinasjonRediger } from './destinasjon/destinasjonRediger/destinasjonerRediger.component';
import { LagreRute } from './rute/lagre/lagreRute';
import { LagreOrdre } from './ordre/lagre/lagreOrdre';
import { VisOrdre } from './ordre/liste/visOrdre';
import { AvgangLagre } from './avgang/avgangLagre/avgangLagre';
import { EndreRute } from './rute/endre/endreRute';
import { EndreOrdre } from './ordre/endre/endreOrdre';
import { AvgangRediger } from './avgang/avgangRediger/avgangRediger';

const appRoots: Routes = [
  { path: 'adminpage', component: AdminpageComponent },
  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'rute', component: ListeRute },
  { path: 'avgang', component: AvgangComponent },
  { path: 'destinasjonListe', component: DestinasjonComponent },
  { path: 'destinasjonLagre', component: DestinasjonLagre },
  { path: 'destinasjonRediger/:id', component: DestinasjonRediger },
  { path: 'lagreRute', component: LagreRute },
  { path: 'nyOrdre', component: LagreOrdre },
  { path: 'visOrdre', component: VisOrdre },
  { path: 'avgangLagre', component: AvgangLagre },
  { path: 'endreRute/:id', component: EndreRute },
  { path: 'endreOrdre/:id', component: EndreOrdre },
  { path: 'visOrdre', component: VisOrdre },
  { path: 'avgangLagre', component: AvgangLagre },
  { path: 'avgangRediger/:id', component: AvgangRediger }

  //Husk eks 'rediger/:id' send med f.eks id i rediger.
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
