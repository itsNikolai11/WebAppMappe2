import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { avgang } from "../../avgang";
import { rute } from "../../rute";

@Component({
  templateUrl: "avgangRediger.html"
})

export class AvgangRediger {
  skjema: FormGroup;
  public ruter: Array<rute>;

  validering = {
    id: [""],
    rute: [""],
    tid: [""]
  }

  constructor(private http: HttpClient, private fb: FormBuilder, private route: ActivatedRoute, private router: Router) {
    this.skjema = fb.group(this.validering);
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.redigerAvgang(params.id);
    });
    this.hentRuter();
  }

  vedSubmit() {
    this.redigerEnAvgang();
  }

  hentRuter() {
    this.http.get<rute[]>("api/rute/")
      .subscribe(data => {
        this.ruter = data;
      },
        error => console.log(error)
      );
  };

  redigerAvgang(id: number) {
    this.http.get<avgang>("api/Avgang" + id).subscribe(avgang => {
      this.skjema.patchValue({ id: avgang.id });
      this.skjema.patchValue({ rute: avgang.ruteNr });
      this.skjema.patchValue({ tid: avgang.avgangTid });
    },
      error => console.log(error)
    );
  }

  redigerEnAvgang() {
    const endretAvgang = new avgang();

    var ruteNr = parseInt(this.skjema.value.rute);

    endretAvgang.id = this.skjema.value.id;
    endretAvgang.ruteNr = ruteNr;
    endretAvgang.avgangTid = this.skjema.value.tid;

    console.log(endretAvgang);

    this.http.put("api/Avgang", endretAvgang)
      .subscribe(
        retur => {
          this.router.navigate(['/avgang']);
        },
        error => console.log(error)
      );

  }
}
