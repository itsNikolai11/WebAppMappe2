import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { destinasjon } from '../../destinasjon';


@Component({
  selector: 'app-root',
  templateUrl: './destinasjonLagre.html'
})

export class DestinasjonLagre {
  skjema: FormGroup;

  validering = {
    id: [""],
    sted: [
      null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
    ],
    land: [
      null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
    ]
  }

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router) {
    this.skjema = fb.group(this.validering);
  }

  vedSubmit() {
    this.lagreDestinasjon();
  }

  lagreDestinasjon() {
    const lagretDestinasjon = new destinasjon();

    lagretDestinasjon.sted = this.skjema.value.sted;
    lagretDestinasjon.land = this.skjema.value.land;

    this.http.post("api/destinasjon", lagretDestinasjon)
      .subscribe(retur => {
        this.router.navigate(['/destinasjonListe']);
      },
        error => console.log(error)
      );
  };
}
