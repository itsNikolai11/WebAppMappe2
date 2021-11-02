import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'admin-mainpage',
  templateUrl: './adminpage.component.html'
})
export class AdminpageComponent {
  constructor(private router: Router, private http: HttpClient) {

  }
  editRuter() {
    this.router.navigate(['/rute']);
  }
  editDestinasjoner() {
    this.router.navigate(['/destinasjonListe']);
  }
  editAvganger() {
    this.router.navigate(['/avgang'])
  }
  loggUt() {
    this.http.get<any>("api/bruker").subscribe(data => {

    }, error => {
      alert(error);
    });
    this.router.navigate(['/']);
  }
}
