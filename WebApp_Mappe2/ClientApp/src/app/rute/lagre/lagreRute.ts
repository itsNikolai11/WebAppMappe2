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

    validering = {
        id: [""],
        fraDestinasjon: [
          ""
        ],
        tilDestinasjon: [
          ""
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

    ngOnInit() {
        this.hentDestinasjoner();
        
    }
    hentDestinasjoner() {
        this.http.get<destinasjon[]>("api/destinasjon/")
            .subscribe(dest => {
                this.destinasjoner = dest;
                
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

        //sjekke om til og fra destinasjon er like
        //sjekke om rute allerede eksisterer!!

        if (lagretRute.fraDestinasjon != lagretRute.tilDestinasjon) {
            this.http.post("api/rute/", lagretRute)
                .subscribe(retur => {
                    this.router.navigate(['/rute']);
                },
                    error => console.log(error)
                );
        }
        return "test"; //må få ut tekst under skjemaet

        
    };
}