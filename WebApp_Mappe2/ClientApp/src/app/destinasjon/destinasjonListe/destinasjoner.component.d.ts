import { HttpClient } from '@angular/common/http';
import { destinasjon } from "../../destinasjon";
import { Router } from '@angular/router';
export declare class DestinasjonComponent {
    private http;
    private router;
    alleDestinasjoner: Array<destinasjon>;
    laster: string;
    constructor(http: HttpClient, router: Router);
    ngOnInit(): void;
    hentAlleDestinasjoner(): void;
    slettDestinasjon(id: number): void;
}
