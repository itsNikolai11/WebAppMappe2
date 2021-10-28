"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.LoginComponent = void 0;
var http_1 = require("@angular/common/http");
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var router_1 = require("@angular/router");
var bruker_1 = require("../bruker");
var LoginComponent = /** @class */ (function () {
    function LoginComponent(_http, fb, router) {
        this._http = _http;
        this.fb = fb;
        this.router = router;
        this.Skjema = fb.group({
            brukernavn: ["", forms_1.Validators.required],
            passord: ["", forms_1.Validators.required]
        });
    }
    LoginComponent.prototype.resetSkjema = function () {
        this.status = "";
    };
    LoginComponent.prototype.login = function () {
        var _this = this;
        var bruker = new bruker_1.Bruker();
        bruker.brukernavn = this.Skjema.value.brukernavn;
        bruker.passord = this.Skjema.value.passord;
        this._http.post("api/bruker", bruker, { observe: 'response' }).subscribe(function (ok) {
            _this.router.navigate(['/adminpage']);
        }, function (error) {
            if (error.status == 401) {
                _this.Skjema.value.brukernavn = "";
                _this.Skjema.value.passord = "";
                _this.status = "Ugyldig brukernavn eller passord!";
            }
            else {
                _this.Skjema.value.brukernavn = "";
                _this.Skjema.value.passord = "";
                _this.status = "Feil på server! Prøv igjen senere";
            }
        });
    };
    ;
    LoginComponent = __decorate([
        (0, core_1.Component)({
            selector: 'admin-login',
            templateUrl: './login.component.html'
        }),
        __metadata("design:paramtypes", [http_1.HttpClient, forms_1.FormBuilder, router_1.Router])
    ], LoginComponent);
    return LoginComponent;
}());
exports.LoginComponent = LoginComponent;
//# sourceMappingURL=login.component.js.map