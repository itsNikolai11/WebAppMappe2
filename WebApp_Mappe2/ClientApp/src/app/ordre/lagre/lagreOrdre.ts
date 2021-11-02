import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { rute } from "../../rute";
import { avgang } from "../../avgang";

@Component({
  templateUrl: "lagreOrdre.html"
})
export class LagreOrdre {

  skjema: FormGroup;
  ruter: Array<rute>;
  avganger: Array<avgang>;
  validering = {
    rute: [null, Validators.compose([Validators.required])],
    avgang: [null, Validators.compose([Validators.required])]
  }

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router) {
    this.skjema = fb.group(this.validering);
  }
  ngOnInit() {
    this.hentRuter();
  }
  hentAvganger(avgang) {
    this.http.get<avgang[]>("api/avgang")
      .subscribe(data => {
        this.avganger = data;
      },
        error => {
          alert(error);
        }
      );
  }
  hentRuter() {
    this.http.get<rute[]>("api/rute")
      .subscribe(data => {
        this.ruter = data;
      }, error => {
        alert(error);
      });

  }
}
