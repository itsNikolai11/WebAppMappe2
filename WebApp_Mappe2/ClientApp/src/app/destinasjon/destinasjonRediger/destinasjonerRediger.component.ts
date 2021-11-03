import { Component } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { destinasjon } from "../../destinasjon";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";


@Component({
  selector: 'app-root',
  templateUrl: "./destinasjonerRediger.component.html"
})

export class DestinasjonRediger {
  skjema: FormGroup;

  //TODO Validering av input med RegEx
  validering = {
    id: [""],
    sted: [
      null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\. ]{2,30}")])
    ],
    land: [
      null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\. ]{2,30}")])
    ]
  }

  constructor(private http: HttpClient, private fb: FormBuilder,
    private route: ActivatedRoute, private router: Router) {
    this.skjema = fb.group(this.validering);
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.redigerDest(params.id);
    })
  }

  vedSubmit() {
    this.redigerEnDest();
  }
 
  redigerDest(id: number) {
    this.http.get<destinasjon>("api/destinasjon/" + id)
      .subscribe(
        destinasjon => {
          this.skjema.patchValue({ id: destinasjon.id });
          this.skjema.patchValue({ sted: destinasjon.sted });
          this.skjema.patchValue({ land: destinasjon.land });
        },
        error => console.log(error)
      );
  }

  redigerEnDest() {
    const redigertDest = new destinasjon();
    redigertDest.id = this.skjema.value.id;
    redigertDest.sted = this.skjema.value.sted;
    redigertDest.land = this.skjema.value.land;


    this.http.put<destinasjon[]>("api/Destinasjon/", redigertDest)
      .subscribe(retur => {
        this.router.navigate(['/destinasjonListe'])
      },
        error => console.log(error),
        () => console.log("Redigert en destinasjon")
      );
  }
}
