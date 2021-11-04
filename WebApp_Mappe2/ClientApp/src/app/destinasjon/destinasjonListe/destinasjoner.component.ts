import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { destinasjon } from "../../destinasjon";
import { Router } from '@angular/router';
import { Modal } from '../../slettModal';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-root',
  templateUrl: './destinasjoner.component.html'
})

export class DestinasjonComponent {
  public alleDestinasjoner: Array<destinasjon>;
  public laster: string;
  destTilSletting: string;

  constructor(private http: HttpClient, private router: Router, private modalService: NgbModal) { }

  ngOnInit() {
    this.hentAlleDestinasjoner();
  }

  hentAlleDestinasjoner() {
    this.laster = "Vennligst vent";
    this.http.get<destinasjon[]>("api/Destinasjon/")
      .subscribe(data => {
        this.alleDestinasjoner = data;
        this.laster = "";
      },
        error => alert(error),
        () => console.log("Alle destinasjoner har blitt hentet.")
      );
  }

  slettDestinasjon(id: number) {
    this.http.get<destinasjon>('api/Destinasjon/' + id)
      .subscribe(destinasjon => {
        this.destTilSletting = destinasjon.sted + " - " + destinasjon.land;
        this.modalSlett(id);

      },
        error => console.log(error)
      );
  }

  modalSlett(id: number) {

    const modalRef = this.modalService.open(Modal);

    modalRef.componentInstance.navn = this.destTilSletting;

    modalRef.result.then(retur => {
      console.log('Lukket med: ' + retur);
      if (retur == "Slett") {

        this.http.delete('api/Destinasjon/' + id)
          .subscribe(retur => {
            this.hentAlleDestinasjoner();
            this.router.navigate(['/destinasjonListe']);
          },
            error => console.log(error),
            () => console.log("Sletting av id:  " + id + " gjennomført.")
          );
      }
      this.hentAlleDestinasjoner();
      this.router.navigate(['/destinasjonListe']);
    });
  }
}
