import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'admin-mainpage',
  templateUrl: './adminpage.component.html'
})
export class AdminpageComponent {
  constructor(private router: Router, private _http: HttpClient) {

  }
  editRuter() {
    this.router.navigate(['/rute']);
  }
  editDestinasjoner() {
    this.router.navigate(['/destinasjon']);
  }
  editAvganger() {

  }
  loggUt() {
    //this._http.get<any>('api/bruker/loggUt').subscribe(data => {
      this.router.navigate(['/']);
    //});
  }
}
