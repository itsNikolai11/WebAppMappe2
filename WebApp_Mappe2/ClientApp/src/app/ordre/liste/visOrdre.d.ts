import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ordre } from '../../ordre';
export declare class VisOrdre {
    private http;
    private router;
    alleOrdre: Array<ordre>;
    constructor(http: HttpClient, router: Router);
    ngOnInit(): void;
    lastOrdre(): void;
    slettOrdre(id: number): void;
}
