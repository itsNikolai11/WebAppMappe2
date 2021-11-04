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
exports.DestinasjonLagre = void 0;
var http_1 = require("@angular/common/http");
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var router_1 = require("@angular/router");
var destinasjon_1 = require("../../destinasjon");
var DestinasjonLagre = /** @class */ (function () {
    function DestinasjonLagre(http, fb, router) {
        this.http = http;
        this.fb = fb;
        this.router = router;
        this.validering = {
            id: [""],
            sted: [
                null, forms_1.Validators.compose([forms_1.Validators.required, forms_1.Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
            ],
            land: [
                null, forms_1.Validators.compose([forms_1.Validators.required, forms_1.Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
            ]
        };
        this.skjema = fb.group(this.validering);
    }
    DestinasjonLagre.prototype.vedSubmit = function () {
        this.lagreDestinasjon();
    };
    DestinasjonLagre.prototype.lagreDestinasjon = function () {
        var lagretDestinasjon = new destinasjon_1.destinasjon();
        lagretDestinasjon.sted = this.skjema.value.sted;
        lagretDestinasjon.land = this.skjema.value.land;
        this.http.post("api/destinasjon", lagretDestinasjon)
            .subscribe(function (retur) {
        }, function (error) { return console.log(error); }, function () { return console.log("Lagring av " + " " + lagretDestinasjon.sted
            + " " + lagretDestinasjon.land + " gjennomført"); });
        this.router.navigate(['/destinasjonListe']);
    };
    ;
    DestinasjonLagre = __decorate([
        (0, core_1.Component)({
            selector: 'app-root',
            templateUrl: './destinasjonLagre.html'
        }),
        __metadata("design:paramtypes", [http_1.HttpClient, forms_1.FormBuilder, router_1.Router])
    ], DestinasjonLagre);
    return DestinasjonLagre;
}());
exports.DestinasjonLagre = DestinasjonLagre;
//# sourceMappingURL=destinasjonLagre.js.map