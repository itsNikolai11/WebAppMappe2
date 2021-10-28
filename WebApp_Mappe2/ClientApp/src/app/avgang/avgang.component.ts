import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { avgang } from "../avgang";

@Component({
  selector: 'app-root',
  templateUrl: './avgang.component.html'
})

export class AvgangComponent {
  public alleAvganger: Array<avgang>;
  public laster: string;

  constructor(private _http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.hentAvganger();
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
}
