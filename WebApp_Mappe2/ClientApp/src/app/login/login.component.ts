import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Bruker } from '../bruker';
@Component({
  selector: 'admin-login',
  templateUrl: './login.component.html'
})
export class LoginComponent{
  Skjema: FormGroup;
  public status: string;
  public gyldig: boolean;
  constructor(private _http: HttpClient, private fb: FormBuilder) {
    this.Skjema = fb.group({
      brukernavn: ["", Validators.required],
      passord: ["", Validators.required]
    });

  }
  login() {
    const bruker = new Bruker();
    bruker.brukernavn = this.Skjema.value.brukernavn;
    bruker.passord = this.Skjema.value.passord;
    this._http.post("api/bruker", bruker, { observe: 'response' }).subscribe(data => {
      console.log(data.status);
    }, error => {
      if (error.status == 401) {
        this.Skjema.value.brukernavn = "";
        this.Skjema.value.passord = "";
        this.status = "Ugyldig brukernavn eller passord!"
      } else {
        this.Skjema.value.brukernavn = "";
        this.Skjema.value.passord = "";
        this.status = "Feil på server! Prøv igjen senere"
      }
    });
  };
}
