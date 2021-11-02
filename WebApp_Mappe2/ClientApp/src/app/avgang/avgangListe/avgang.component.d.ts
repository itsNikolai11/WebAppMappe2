import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { avgang } from "../../avgang";
import { rute } from "../../rute";
export declare class AvgangComponent {
    private _http;
    private router;
    ruter: Array<rute>;
    alleAvganger: Array<avgang>;
    laster: string;
    id: number;
    constructor(_http: HttpClient, router: Router);
    ngOnInit(): void;
    hentRuter(): void;
    hentAvganger(): void;
    onChange(event: number): void;
}
