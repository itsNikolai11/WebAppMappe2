import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { rute } from "../../rute";

@Component({
    templateUrl: "lagreRute.html"
})

export class LagreRute {
    skjema: FormGroup;

    validering = {
        id: [""],
        fraDestinasjon: [
          null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
        ],
        tilDestinasjon: [
          null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
        ],
        prisBarn: [
            null, Validators.compose([Validators.required, Validators.pattern("[0-9]{1,5}")])
        ],
        prisVoksen: [
            null, Validators.compose([Validators.required, Validators.pattern("[0-9]{1,5}")])
        ]
        
    }
        

    constructor(private http: HttpClient, private fb: FormBuilder, private router: Router) {
        this.skjema = fb.group(this.validering);
    }

    vedSubmit() {
        this.lagreRute();
    }

    lagreRute() {
        const lagretRute = new rute();
        lagretRute.fraDestinasjon = this.skjema.value.fraDestinasjon;
        lagretRute.tilDestinasjon = this.skjema.value.tilDestinasjon;
        lagretRute.prisBarn = this.skjema.value.prisBarn;
        lagretRute.prisVoksen = this.skjema.value.prisVoksen;

        this.http.post("api/Rute/", lagretRute)
            .subscribe(retur => {
                this.router.navigate(['/rute']);
            },
                error => console.log(error)
            );
    };
}