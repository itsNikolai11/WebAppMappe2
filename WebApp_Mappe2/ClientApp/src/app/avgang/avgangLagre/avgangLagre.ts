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

   /*var date = new Date(this.skjema.value.tid);
    var day = date.getDate();       // yields date
    var month = date.getMonth() + 1;    // yields month (add one as '.getMonth()' is zero indexed)
    var year = date.getFullYear();  // yields year
    var hour = date.getHours();     // yields hours 
    var minute = date.getMinutes(); // yields minutes

    var time = day + "/" + month + "/" + year + " " + hour + ':' + minute + ':' + '00';*/

    var ruteNr = parseInt(this.skjema.value.rute);
    lagretAvgang.ruteNr = ruteNr;


    //lagretAvgang.avgangTid = time; //this.skjema.value.tid;

    var date = new Date(this.skjema.value.tid);
    lagretAvgang.avgangTid = date.toJSON();

    console.log(lagretAvgang.avgangTid);
    console.log(lagretAvgang.ruteNr)

    console.log(lagretAvgang);

    this.http.post("api/Avgang", lagretAvgang )
      .subscribe(retur => {
        this.router.navigate(['/avgang']);
      },
        error => console.log(error)
      );
  };
}
