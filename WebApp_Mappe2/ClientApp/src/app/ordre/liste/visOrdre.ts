import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ordre } from '../../ordre';
import { rute } from '../../rute';
@Component({
  templateUrl: "visOrdre.html"
})
export class VisOrdre {
  public alleOrdre: Array<ordre>;
  public ruter: Array<rute>;

  constructor(private http: HttpClient, private router: Router) {

  }
  ngOnInit() {
    this.lastOrdre();
  }
  lastOrdre() {
    this.http.get<ordre[]>("api/ordre").subscribe(data => {
      this.alleOrdre = data;
    });
    this.lastRuter();
  }
  lastRuter() {
    this.http.get<rute[]>("api/rute").subscribe(data => {
      this.ruter = data;
    });
  }
  filtrerOrdre(ruteId: number) {

  }
  slettOrdre(id: number) {
    this.http.delete('api/ordre/' + id).subscribe(data => {
      this.lastOrdre();
      this.router.navigate(['/visOrdre']);
    }, error => {
      alert(error);
    });
    
  }
}
