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

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.hentAlleDestinasjoner();
  }

  hentAlleDestinasjoner() {
    this.laster = "Vennligst vent";
    this.http.get<destinasjon[]>("api/Destinasjon/")
      .subscribe(data => {
        this.alleDestinasjoner = data;
        this.laster = "";
      },
      error => alert(error),
      () => console.log("Alle destinasjoner har blitt hentet.")
    );
  }

  slettDestinasjon(id: number){
    this.http.delete('api/Destinasjon/' + id)
      .subscribe(retur => {
        this.hentAlleDestinasjoner();
        this.router.navigate(['/destinasjonListe']);
      },
        error => console.log(error),
          () => console.log("Sletting av id:  " + id + " gjennomført.")
      );
  }
}

