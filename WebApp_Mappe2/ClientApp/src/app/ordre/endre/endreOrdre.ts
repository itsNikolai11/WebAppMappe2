import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { avgang } from '../../avgang';
import { ordre } from '../../ordre';
import { rute } from '../../rute';
@Component({
  templateUrl: "endreOrdre.html"
})
export class EndreOrdre {
  skjema: FormGroup;
  ruter: Array<rute>;
  avganger: Array<avgang>;
  validering = {
    id: [""],
    rute: [null, Validators.compose([Validators.required])],
    avgang: [null, Validators.compose([Validators.required])],
    antallBarn: [null, Validators.compose([Validators.required, Validators.pattern("[0-9]{1,5}"), Validators.min(1)])],
    antallVoksne: [null, Validators.compose([Validators.required, Validators.pattern("[0-9]{1,5}"), Validators.min(1)])],
    refPers: [null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])]
  }
  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) {
    this.skjema = fb.group(this.validering);
  }
  ngOnInit() {
    this.route.params.subscribe(params => {
      this.lastFelt(params.id);
    });
    this.hentRuter();
  }
  lastFelt(ordreId: number) {
    this.http.get<ordre>('api/ordre/' + ordreId).subscribe(ordre => {
      this.hentAvganger(ordre.ruteNr);
      this.skjema.patchValue({ id: ordre.id });
      this.skjema.patchValue({ antallBarn: ordre.antallBarn });
      this.skjema.patchValue({ antallVoksne: ordre.antallVoksen });
      this.skjema.patchValue({ refPers: ordre.refPers });
      this.skjema.patchValue({ rute: ordre.ruteNr });
      this.skjema.patchValue({ avgang: ordre.avgangNr });
    }, error => {
      alert(error);
    }
      );
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
    nyOrdre.id = this.skjema.value.id;
    nyOrdre.refPers = this.skjema.value.refPers;
    nyOrdre.ruteNr = this.skjema.value.rute;
    nyOrdre.avgangNr = this.skjema.value.avgang;
    nyOrdre.antallBarn = this.skjema.value.antallBarn;
    nyOrdre.antallVoksen = this.skjema.value.antallVoksne;
    this.http.put('api/ordre', nyOrdre).subscribe(retur => {
      this.router.navigate(["/visOrdre"]);
    }, error => {
      alert(error);
    });
  }
}
