import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { avgang } from "../../avgang";
import { rute } from "../../rute";
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Modal } from '../../slettModal';

@Component({
  selector: 'app-root',
  templateUrl: './avgang.component.html'
})

export class AvgangComponent {
  public ruter: Array<rute>;
  public alleAvganger: Array<avgang>;
  public laster: string;
  public id: number;
  avgangTilSletting: string;
  slettingOK: boolean;
  public onChangeValue: number;

  constructor(private http: HttpClient, private router: Router, private modalService: NgbModal) { }

  ngOnInit() {
    this.hentRuter();
    this.hentAvganger();
  }

  hentRuter() {
    this.http.get<rute[]>("api/rute/")
      .subscribe(data => {
        this.ruter = data;
        console.log(data);
      },
        error => alert(error),
        () => console.log("ferdig get-/ruter")
      );
  }

  hentAvganger() {
    this.laster = "Vennligst vent";
    this.http.get<avgang[]>("api/Avgang/")
      .subscribe(data => {
        this.alleAvganger = data;
        this.laster = "";
        console.log(data);
      },
        error => alert(error),
        () => console.log("ferdig get-/avganger")
    );
  }

  onChange(event : number) {
    this.id = event;
    console.log(this.id);
  }

  sletteAvgang(id: number) {
    this.http.get<avgang>("api/Avgang/" + id)
      .subscribe(data => {
        for (var rute of this.ruter) {
          if (rute.id === data.ruteNr) {
            this.avgangTilSletting = rute.fraDestinasjon + " - " + rute.tilDestinasjon + " , kl: " + data.avgangTid;
          }
        }
        this.visModalOgSlett(id);
      },
        error => console.log(error)
      );
  }

  visModalOgSlett(id: number) {
    const modalRef = this.modalService.open(Modal);

    modalRef.componentInstance.navn = this.avgangTilSletting;

    modalRef.result.then(retur => {
      console.log('Lukket med:' + retur);
      if (retur == "Slett") {
        this.http.delete("api/Avgang/" + id)
          .subscribe(retur => {
            this.hentAvganger();
          },
            error => console.log(error)
          );
      }
      this.router.navigate(['/avgang']);
    });
  }
}
