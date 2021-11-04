import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Modal } from '../../slettModal';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { rute } from "../../rute";

@Component({
  templateUrl: "listeRute.html"
})
export class ListeRute {
  alleRuter: Array<rute>; 
  laster: boolean;
  ruteTilSletting: string;

    constructor(private http: HttpClient, private router: Router, private modalService: NgbModal) { }

  ngOnInit() {
    this.laster = true;
    this.hentAlleRuter();
    
  }

  

  hentAlleRuter() {
    this.http.get<rute[]>("api/rute/")
      .subscribe(rutene => {
        
        this.alleRuter = rutene; 
        this.laster = false;
        
      },
        error => console.log(error)
      );
  };

 

    slettRute(id: number) {

        
        this.http.get<rute>('api/rute/' + id)
            .subscribe(rute => {
                this.ruteTilSletting = rute.fraDestinasjon + " - " + rute.tilDestinasjon;
                this.modalSlett(id);
            },
                error => console.log(error)
            );
    }

    modalSlett(id: number) {
        const modalRef = this.modalService.open(Modal);

        modalRef.componentInstance.navn = this.ruteTilSletting;

        modalRef.result.then(retur => {
            console.log('Lukket med:' + retur);
            if (retur == "Slett") {

                this.http.delete('api/rute/' + id)
                    .subscribe(retur => {
                        this.hentAlleRuter();
                    },
                        error => console.log(error)
                    );
            }
            this.router.navigate(['/rute']);
            
        });
    } 

}
