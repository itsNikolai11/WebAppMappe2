import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ordre } from '../../ordre';
import { rute } from '../../rute';
import { Modal } from '../../slettModal';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  templateUrl: "visOrdre.html"
})
export class VisOrdre {
  public alleOrdre: Array<ordre>;
  public ruter: Array<rute>;
  public ordreTilSletting: string;

  constructor(private http: HttpClient, private router: Router, private modalService: NgbModal) {

  }
  ngOnInit() {
    this.lastOrdre();
  }
  lastOrdre() {
    this.http.get<ordre[]>("api/ordre").subscribe(data => {
      this.alleOrdre = data;
    });
    this.lastRuter();
  }
  lastRuter() {
    this.http.get<rute[]>("api/rute").subscribe(data => {
      this.ruter = data;
    });
  }
  filtrerOrdre(ruteId: number) {

  }
  slettOrdre(id: number) {
    this.http.get<ordre>('api/ordre/' + id)
      .subscribe(data => {
        this.ordreTilSletting = data.refPers + " sin bestilling";
        this.visModalOgSlett(id);
      },
        error => console.log(error)
      );
    
  }

  visModalOgSlett(id: number) {
    const modalRef = this.modalService.open(Modal);

    modalRef.componentInstance.navn = this.ordreTilSletting;

    modalRef.result.then(retur => {
      console.log('Lukket med:' + retur);
      if (retur == "Slett") {

        // kall til server for sletting
        this.http.delete("api/ordre/" + id)
          .subscribe(retur => {
            this.lastOrdre();
          },
            error => console.log(error)
          );
      }
      this.router.navigate(['/visOrdre']);
    });
  }
}
