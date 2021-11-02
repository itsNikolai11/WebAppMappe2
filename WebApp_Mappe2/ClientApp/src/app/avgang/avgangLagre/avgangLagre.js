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
exports.AvgangLagre = void 0;
var core_1 = require("@angular/core");
var http_1 = require("@angular/common/http");
var forms_1 = require("@angular/forms");
var router_1 = require("@angular/router");
var avgang_1 = require("../../avgang");
var AvgangLagre = /** @class */ (function () {
    function AvgangLagre(http, fb, router) {
        this.http = http;
        this.fb = fb;
        this.router = router;
        this.validering = {
            id: [""],
            rute: [""],
            tid: [""]
        };
        this.skjema = fb.group(this.validering);
    }
    AvgangLagre.prototype.ngOnInit = function () {
        this.hentRuter();
        this.hentAvganger();
    };
    AvgangLagre.prototype.hentRuter = function () {
        var _this = this;
        this.http.get("api/rute/")
            .subscribe(function (data) {
            _this.ruter = data;
            console.log(data);
        }, function (error) { return alert(error); }, function () { return console.log("ferdig get-/ruter"); });
    };
    AvgangLagre.prototype.hentAvganger = function () {
        var _this = this;
        this.http.get("api/Avgang/")
            .subscribe(function (avgang) {
            _this.alleAvganger = avgang;
        }, function (error) { return console.log(error); });
    };
    ;
    AvgangLagre.prototype.vedSubmit = function () {
        this.lagreAvgang();
    };
    AvgangLagre.prototype.lagreAvgang = function () {
        var lagretAvgang = new avgang_1.avgang();
        lagretAvgang.ruteNr = this.skjema.value.rute;
        lagretAvgang.avgangTid = this.skjema.value.tid;
        console.log(lagretAvgang);
        /*this.http.post("api/Avgang", lagretAvgang)
          .subscribe(retur => {
            this.router.navigate(['/avgang']);
          },
            error => console.log(error)
          );*/
    };
    ;
    AvgangLagre = __decorate([
        (0, core_1.Component)({
            templateUrl: "avgangLagre.html"
        }),
        __metadata("design:paramtypes", [http_1.HttpClient, forms_1.FormBuilder, router_1.Router])
    ], AvgangLagre);
    return AvgangLagre;
}());
exports.AvgangLagre = AvgangLagre;
//# sourceMappingURL=avgangLagre.js.map