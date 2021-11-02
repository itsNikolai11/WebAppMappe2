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
exports.DestinasjonComponent = void 0;
var core_1 = require("@angular/core");
var http_1 = require("@angular/common/http");
var router_1 = require("@angular/router");
var DestinasjonComponent = /** @class */ (function () {
    function DestinasjonComponent(http, router) {
        this.http = http;
        this.router = router;
    }
    DestinasjonComponent.prototype.ngOnInit = function () {
        this.hentAlleDestinasjoner();
    };
    DestinasjonComponent.prototype.hentAlleDestinasjoner = function () {
        var _this = this;
        this.laster = "Vennligst vent";
        this.http.get("api/Destinasjon/")
            .subscribe(function (data) {
            _this.alleDestinasjoner = data;
            _this.laster = "";
        }, function (error) { return alert(error); }, function () { return console.log("Alle destinasjoner har blitt hentet."); });
    };
    DestinasjonComponent.prototype.slettDestinasjon = function (id) {
        this.http.delete("api/Destinasjon/" + id)
            .subscribe(function (retur) {
            //this.hentAlleDestinasjoner();
        }, function (error) { return console.log(error); }, function () { return console.log("Sletting av id:  " + id + " gjennomført."); });
    };
    DestinasjonComponent = __decorate([
        (0, core_1.Component)({
            selector: 'app-root',
            templateUrl: './destinasjoner.component.html'
        }),
        __metadata("design:paramtypes", [http_1.HttpClient, router_1.Router])
    ], DestinasjonComponent);
    return DestinasjonComponent;
}());
exports.DestinasjonComponent = DestinasjonComponent;
//# sourceMappingURL=destinasjoner.component.js.map