import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
export declare class DestinasjonRediger {
    private http;
    private fb;
    private route;
    private router;
    skjema: FormGroup;
    validering: {
        id: string[];
        sted: import("@angular/forms").ValidatorFn[];
        land: import("@angular/forms").ValidatorFn[];
    };
    constructor(http: HttpClient, fb: FormBuilder, route: ActivatedRoute, router: Router);
    ngOnInit(): void;
    vedSubmit(): void;
    redigerDest(id: number): void;
    redigerEnDest(): void;
}
