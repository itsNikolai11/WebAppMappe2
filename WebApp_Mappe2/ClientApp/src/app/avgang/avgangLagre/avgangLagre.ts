import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { avgang } from "../../avgang";
import { rute } from '../../rute';

@Component({
  templateUrl: "avgangLagre.html"
})
export class AvgangLagre {
  skjema: FormGroup;
  public alleAvganger: Array<avgang>;
  public ruter: Array<rute>;

  validering = {
    id: [""],
    rute: [""],
    tid: [""]
  }

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router) {
    this.skjema = fb.group(this.validering);
  }

  ngOnInit() {
    this.hentRuter()
    this.hentAvganger();
  }

  hentRuter() {
    this.http.get<rute[]>("api/rute/")
      .subscribe(data => {
        this.ruter = data;
        console.log(data);
      },
        error => alert(error),
        () => console.log("ferdig get-/ruter")
      );
  }

  hentAvganger() {
    this.http.get<avgang[]>("api/Avgang/")
      .subscribe(avgang => {
        this.alleAvganger = avgang;
      },
        error => console.log(error)
      );
  };


  vedSubmit() {
    this.lagreAvgang();
  }

  lagreAvgang() {
    const lagretAvgang = new avgang();

    
    lagretAvgang.ruteNr = this.skjema.value.rute;
    lagretAvgang.avgangTid = this.skjema.value.tid;

    /*for (let avgang of this.alleAvganger) {

      console.log(avgang.avgangTid.toString());
      var tid = avgang.avgangTid.toString();
      var tid1 = lagretAvgang.avgangTid.toString() + ":00";
      console.log(tid1);
      if (tid === tid1 && avgang.ruteNr === lagretAvgang.ruteNr) {
        console.log("feil");
        return;
      }
    }*/
    this.http.post("api/Avgang/", lagretAvgang)
      .subscribe(retur => {
        console.log(lagretAvgang);
        this.router.navigate(['/avgang']);
      },
        error => console.log(error)
      );
  };
}
