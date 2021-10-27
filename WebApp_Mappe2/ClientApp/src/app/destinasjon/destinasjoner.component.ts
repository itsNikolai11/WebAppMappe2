import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { destinasjon } from "../destinasjon";

@Component({
  selector: 'app-root',
  templateUrl: './destinasjoner.component.html'
})

export class DestinasjonComponent {
  public alleDestinasjoner: Array<destinasjon>;
  public laster: string;

  constructor(private _http: HttpClient) { }

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
      () => console.log("ferdig get-/destinajon")
    );
  }
}

