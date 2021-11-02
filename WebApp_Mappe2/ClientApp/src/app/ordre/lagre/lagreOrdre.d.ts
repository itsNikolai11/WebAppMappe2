import { HttpClient } from '@angular/common/http';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { rute } from "../../rute";
import { avgang } from "../../avgang";
export declare class LagreOrdre {
    private http;
    private fb;
    private router;
    skjema: FormGroup;
    ruter: Array<rute>;
    avganger: Array<avgang>;
    validering: {
        rute: import("@angular/forms").ValidatorFn[];
        avgang: import("@angular/forms").ValidatorFn[];
        antallBarn: import("@angular/forms").ValidatorFn[];
        antallVoksne: import("@angular/forms").ValidatorFn[];
        refPers: import("@angular/forms").ValidatorFn[];
    };
    constructor(http: HttpClient, fb: FormBuilder, router: Router);
    ngOnInit(): void;
    hentAvganger(avgangId: number): void;
    filtrerAvganger(avganger: Array<avgang>, id: number): void;
    hentRuter(): void;
    onSubmit(): void;
    lagreOrdre(): void;
}
