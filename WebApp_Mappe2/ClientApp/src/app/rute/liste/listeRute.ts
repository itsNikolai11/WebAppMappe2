import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
//import { Modal } from './sletteModal';

import { rute } from "../../rute";

@Component({
  templateUrl: "listeRute.html"
})
export class ListeRute {
  alleRuter: Array<rute>;
  laster: boolean;
  //kundeTilSletting: string;
  //slettingOK: boolean;

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.laster = true;
    this.hentAlleRuter();
  }

  hentAlleRuter() {
    this.http.get<rute[]>("api/Rute/")
      .subscribe(rutene => {
        this.alleRuter = rutene;
        this.laster = false;
      },
        error => console.log(error)
      );
  };

 

  slettRute(id: number) {
    this.http.delete("api/Rute/" + id)
      .subscribe(retur => {
       this.hentAlleRuter();
        this.router.navigate(['/rute']);
      },
        error => console.log(error)
      );
  };

}
