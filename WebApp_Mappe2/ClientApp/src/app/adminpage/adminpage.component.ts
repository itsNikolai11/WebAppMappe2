import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'admin-mainpage',
  templateUrl: './adminpage.component.html'
})
export class AdminpageComponent {
  constructor(private router: Router) {

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
    this.router.navigate(['/']);
    //TODO set session-attributt
  }
}
