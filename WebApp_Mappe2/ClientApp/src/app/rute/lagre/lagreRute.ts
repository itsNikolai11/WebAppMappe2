import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { rute } from "../../rute";
import { destinasjon } from "../../destinasjon";

@Component({
  templateUrl: "lagreRute.html"
})

export class LagreRute {
  skjema: FormGroup;
  destinasjoner: Array<destinasjon>;
  feilLik: boolean;
  ruter: Array<rute>;
  feilEksisterer: boolean;


  validering = {
    id: [""],
    fraDestinasjon: [
      ""
    ],
    tilDestinasjon: [
      ""],
    prisBarn: [
      null, Validators.compose([Validators.required, Validators.pattern("[0-9]{1,5}"), Validators.min(1)])
    ],
    prisVoksen: [
      null, Validators.compose([Validators.required, Validators.pattern("[0-9]{1,5}"), Validators.min(1)])
    ]

  }


  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router) {
    this.skjema = fb.group(this.validering);
  }

  ngOnInit() {
    this.hentDestinasjoner();
    this.hentAlleRuter();
  }
  hentDestinasjoner() {

    this.http.get<destinasjon[]>("api/destinasjon/")
      .subscribe(dest => {
        this.destinasjoner = dest;
        for (var destin in dest) {

        }

      },
        error => console.log(error)
      );
  };

  hentAlleRuter() {
    this.http.get<rute[]>("api/rute/")
      .subscribe(rutene => {

        this.ruter = rutene;

      },
        error => console.log(error)
      );
  };


  vedSubmit() {
    this.lagreRute();

  }

  lagreRute() {

    const lagretRute = new rute();
        
    lagretRute.fraDestinasjon = this.skjema.value.fraDestinasjon;
    lagretRute.tilDestinasjon = this.skjema.value.tilDestinasjon;
    lagretRute.prisBarn = this.skjema.value.prisBarn;
    lagretRute.prisVoksen = this.skjema.value.prisVoksen;

    for (let rute of this.ruter) {
      if (rute.fraDestinasjon === lagretRute.fraDestinasjon && rute.tilDestinasjon === lagretRute.tilDestinasjon) {

        this.feilEksisterer = true;
        this.feilLik = false;
        return;
      }
    }

    if (lagretRute.fraDestinasjon != lagretRute.tilDestinasjon) {
      this.http.post("api/rute/", lagretRute)
        .subscribe(retur => {
          this.router.navigate(['/rute']);
          
        },
          error => console.log(error)
        );
    }
    else {
      this.feilLik = true;
      this.feilEksisterer = false;
    }
    //this.router.navigate(['/rute']);
  };
}
