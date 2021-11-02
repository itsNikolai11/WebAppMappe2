import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { avgang } from "../../avgang";

@Component({
  templateUrl: "avgangRediger.html"
})

export class AvgangRediger {
  skjema: FormGroup;

  validering = {
    id: [""],
    rute: [""],
    tid: [""]
  }

  constructor(private http: HttpClient, private fb: FormBuilder, private route: ActivatedRoute, private router: Router) {
    this.skjema = fb.group(this.validering);
  }

  ngOnInit() {
    this.redigerEnAvgang();
  }

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
    const endretAvgang = new avgang()
    endretAvgang.id = this.skjema.value.id;
    endretAvgang.ruteNr = this.skjema.value.rute;
    endretAvgang.avgangTid = this.skjema.value.tid;

    this.http.put("api/Avgang", endretAvgang)
      .subscribe(
        retur => {
          this.router.navigate(['/avgang']);
        },
        error => console.log(error)
      );

  }
}
