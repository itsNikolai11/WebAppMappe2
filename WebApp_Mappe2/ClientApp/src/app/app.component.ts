import { Component } from '@angular/core';
import { destinasjon } from "./destinasjon";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})

export class AppComponent {
  public alleDestinasjoner: Array<destinasjon>;
  public laster: string;

  constructor(private _http: HttpClient) { }

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

