import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ordre } from '../../ordre';
@Component({
  templateUrl: "visOrdre.html"
})
export class VisOrdre {
  public alleOrdre: Array<ordre>

  constructor(private http: HttpClient, private router: Router) {

  }
  ngOnInit() {
    this.lastOrdre();
  }
  lastOrdre() {
    this.http.get<ordre[]>("api/ordre").subscribe(data => {
      this.alleOrdre = data;
    })
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
