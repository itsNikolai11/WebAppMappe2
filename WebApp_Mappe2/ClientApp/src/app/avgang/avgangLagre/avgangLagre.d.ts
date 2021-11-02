import { HttpClient } from '@angular/common/http';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { avgang } from "../../avgang";
import { rute } from '../../rute';
export declare class AvgangLagre {
    private http;
    private fb;
    private router;
    skjema: FormGroup;
    alleAvganger: Array<avgang>;
    ruter: Array<rute>;
    validering: {
        id: string[];
        rute: string[];
        tid: string[];
    };
    constructor(http: HttpClient, fb: FormBuilder, router: Router);
    ngOnInit(): void;
    hentRuter(): void;
    hentAvganger(): void;
    vedSubmit(): void;
    lagreAvgang(): void;
}
