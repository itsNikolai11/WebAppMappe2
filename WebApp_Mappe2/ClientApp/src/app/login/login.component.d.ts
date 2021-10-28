import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
export declare class LoginComponent {
    private _http;
    private fb;
    private router;
    Skjema: FormGroup;
    status: string;
    gyldig: boolean;
    constructor(_http: HttpClient, fb: FormBuilder, router: Router);
    resetSkjema(): void;
    login(): void;
}
