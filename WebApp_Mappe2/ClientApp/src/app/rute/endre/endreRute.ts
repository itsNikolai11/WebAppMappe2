import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router'; 
import { rute} from "../../rute";

@Component({
    templateUrl: "endreRute.html"
  
})

export class EndreRute {
  skjema: FormGroup;

    validering = {
        id: [""],
        fraDestinasjon: [
            ""
        ],
        tilDestinasjon: [
            ""
        ],
      prisBarn: [
        null, Validators.compose([Validators.required, Validators.pattern("[0-9]{1,5}"), Validators.min(1)])
        ],
      prisVoksen: [
        null, Validators.compose([Validators.required, Validators.pattern("[0-9]{1,5}"), Validators.min(1)])
      ]
    
  }

    constructor(private http: HttpClient, private fb: FormBuilder, private route: ActivatedRoute, private router: Router) {
        this.skjema = fb.group(this.validering); 
    }
    
   

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.endreRute(params.id);
        });
    }

    vedSubmit() {
        this.endreEnRute(); 
    }

    endreRute(id: number) {
        this.http.get<rute>("api/rute/" + id)
            .subscribe(
                rute => {
                    this.skjema.patchValue({ id: rute.id });
                    this.skjema.patchValue({ fraDestinasjon: rute.fraDestinasjon });
                    this.skjema.patchValue({ tilDestinasjon: rute.tilDestinasjon });
                    this.skjema.patchValue({ prisBarn: rute.prisBarn });
                    this.skjema.patchValue({ prisVoksen: rute.prisVoksen });

                },
                error => console.log(error)
            );
    }

    endreEnRute() {
        const endretRute = new rute();
        endretRute.id = this.skjema.value.id;
        endretRute.prisBarn = this.skjema.value.prisBarn;
        endretRute.prisVoksen = this.skjema.value.prisVoksen;

        this.http.put("api/rute/", endretRute)
          .subscribe(
            retur => {
              this.router.navigate(['/rute']);
            },
            error => console.log(error)
          );
      
      
        
    }
}
