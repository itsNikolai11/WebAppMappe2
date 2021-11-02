import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { rute } from "../../rute";
import { avgang } from "../../avgang";
import { ordre } from "../../ordre";

@Component({
  templateUrl: "lagreOrdre.html"
})
export class LagreOrdre {

  skjema: FormGroup;
  ruter: Array<rute>;
  avganger: Array<avgang>;
  validering = {
    rute: [null, Validators.compose([Validators.required])],
    avgang: [null, Validators.compose([Validators.required])],
    antallBarn: [null, Validators.compose([Validators.required])],
    antallVoksne: [null, Validators.compose([Validators.required])],
    refPers: [null, Validators.compose([Validators.required])]
  }

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router) {
    this.skjema = fb.group(this.validering);
  }
  ngOnInit() {
    this.hentRuter();
  }
  hentAvganger(avgangId: number): void {
    this.http.get<avgang[]>("api/avgang")
      .subscribe(data => {
        this.filtrerAvganger(data, avgangId);
      },
        error => {
          alert(error);
        }
      );
  }
  filtrerAvganger(avganger: Array<avgang>, id: number) {
    const filtrerteAvganger = new Array<avgang>();
    for (let a of avganger) {
      if (a.id == id) {
        filtrerteAvganger.push(a);
      }
    }
    this.avganger = filtrerteAvganger;

  }
  hentRuter() {
    this.http.get<rute[]>("api/rute")
      .subscribe(data => {
        this.ruter = data;
      }, error => {
        alert(error);
      });

  }
  onSubmit() {
    this.lagreOrdre();
  }
  lagreOrdre() {
    const nyOrdre = new ordre();
    nyOrdre.ruteNr = this.skjema.value.rute;
    nyOrdre.avgangNr = this.skjema.value.avgang;
    nyOrdre.antallBarn = this.skjema.value.antallBarn;
    nyOrdre.antallVoksne = this.skjema.value.antallVoksne;
    this.http.post("api/ordre", nyOrdre).subscribe(retur => {
      this.router.navigate(["/visOrdre"]);
    }, error => {
      alert(error);
    });
  }
}
