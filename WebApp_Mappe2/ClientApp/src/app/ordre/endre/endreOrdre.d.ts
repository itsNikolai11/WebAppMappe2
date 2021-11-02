import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { avgang } from '../../avgang';
import { rute } from '../../rute';
export declare class EndreOrdre {
    private http;
    private fb;
    private router;
    private route;
    skjema: FormGroup;
    ruter: Array<rute>;
    avganger: Array<avgang>;
    validering: {
        id: string[];
        rute: import("@angular/forms").ValidatorFn[];
        avgang: import("@angular/forms").ValidatorFn[];
        antallBarn: import("@angular/forms").ValidatorFn[];
        antallVoksne: import("@angular/forms").ValidatorFn[];
        refPers: import("@angular/forms").ValidatorFn[];
    };
    constructor(http: HttpClient, fb: FormBuilder, router: Router, route: ActivatedRoute);
    ngOnInit(): void;
    lastFelt(ordreId: number): void;
    hentAvganger(avgangId: number): void;
    filtrerAvganger(avganger: Array<avgang>, id: number): void;
    hentRuter(): void;
    onSubmit(): void;
    lagreOrdre(): void;
}
