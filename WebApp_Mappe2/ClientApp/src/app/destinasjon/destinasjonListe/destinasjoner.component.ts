import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { destinasjon } from "../../destinasjon";
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './destinasjoner.component.html'
})

export class DestinasjonComponent {
  public alleDestinasjoner: Array<destinasjon>;
  public laster: string;

  constructor(private _http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.hentAlleDestinasjoner();
  }

  hentAlleDestinasjoner() {
    this.laster = "Vennligst vent";
    this._http.get<destinasjon[]>("api/Destinasjon/")
      .subscribe(data => {
        this.alleDestinasjoner = data;
        this.laster = "";
      },
      error => alert(error),
      () => console.log("Hent alle gjennomført gjennomført.")
    );
  }

  slettDestinasjon(id: number){
    this._http.delete("api/Destinasjon/" + id)
      .subscribe(retur => {
        this.router.navigate(['/destinasjon']);
        this.hentAlleDestinasjoner();
      },
       error => console.log(error)
      );
  }
}

