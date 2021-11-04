import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
export declare class DestinasjonLagre {
    private http;
    private fb;
    private router;
    skjema: FormGroup;
    validering: {
        id: string[];
        sted: import("@angular/forms").ValidatorFn[];
        land: import("@angular/forms").ValidatorFn[];
    };
    constructor(http: HttpClient, fb: FormBuilder, router: Router);
    vedSubmit(): void;
    lagreDestinasjon(): void;
}
