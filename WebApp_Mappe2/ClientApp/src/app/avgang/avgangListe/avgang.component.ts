import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { avgang } from "../../avgang";
import { rute } from "../../rute";

@Component({
  selector: 'app-root',
  templateUrl: './avgang.component.html'
})

export class AvgangComponent {
  public ruter: Array<rute>;
  public alleAvganger: Array<avgang>;
  public laster: string;
  public id: number;

  constructor(private _http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.hentRuter();
    this.hentAvganger();
  }

  hentRuter() {
    this._http.get<rute[]>("api/rute/")
      .subscribe(data => {
        this.ruter = data;
        console.log(data);
      },
        error => alert(error),
        () => console.log("ferdig get-/ruter")
      );
  }

  hentAvganger() {
    this.laster = "Vennligst vent";
    this._http.get<avgang[]>("api/Avgang/")
      .subscribe(data => {
        this.alleAvganger = data;
        this.laster = "";
        console.log(data);
      },
        error => alert(error),
        () => console.log("ferdig get-/avganger")
    );
  }

  onChange(event : number) {
    this.id = event;
    console.log(this.id);
  }
}
