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
exports.EndreOrdre = void 0;
var http_1 = require("@angular/common/http");
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var router_1 = require("@angular/router");
var ordre_1 = require("../../ordre");
var EndreOrdre = /** @class */ (function () {
    function EndreOrdre(http, fb, router, route) {
        this.http = http;
        this.fb = fb;
        this.router = router;
        this.route = route;
        this.validering = {
            id: [""],
            rute: [null, forms_1.Validators.compose([forms_1.Validators.required])],
            avgang: [null, forms_1.Validators.compose([forms_1.Validators.required])],
            antallBarn: [null, forms_1.Validators.compose([forms_1.Validators.required])],
            antallVoksne: [null, forms_1.Validators.compose([forms_1.Validators.required])],
            refPers: [null, forms_1.Validators.compose([forms_1.Validators.required])]
        };
        this.skjema = fb.group(this.validering);
    }
    EndreOrdre.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.subscribe(function (params) {
            _this.lastFelt(params.id);
        });
        this.hentRuter();
    };
    EndreOrdre.prototype.lastFelt = function (ordreId) {
        var _this = this;
        this.http.get('api/ordre/' + ordreId).subscribe(function (ordre) {
            _this.hentAvganger(ordre.ruteNr);
            _this.skjema.patchValue({ id: ordre.id });
            _this.skjema.patchValue({ antallBarn: ordre.antallBarn });
            _this.skjema.patchValue({ antallVoksne: ordre.antallVoksen });
            _this.skjema.patchValue({ refPers: ordre.refPers });
            _this.skjema.patchValue({ rute: ordre.ruteNr });
            _this.skjema.patchValue({ avgang: ordre.avgangNr });
        }, function (error) {
            alert(error);
        });
    };
    EndreOrdre.prototype.hentAvganger = function (avgangId) {
        var _this = this;
        this.http.get("api/avgang")
            .subscribe(function (data) {
            _this.filtrerAvganger(data, avgangId);
        }, function (error) {
            alert(error);
        });
    };
    EndreOrdre.prototype.filtrerAvganger = function (avganger, id) {
        var filtrerteAvganger = new Array();
        for (var _i = 0, avganger_1 = avganger; _i < avganger_1.length; _i++) {
            var a = avganger_1[_i];
            if (a.id == id) {
                filtrerteAvganger.push(a);
            }
        }
        this.avganger = filtrerteAvganger;
    };
    EndreOrdre.prototype.hentRuter = function () {
        var _this = this;
        this.http.get("api/rute")
            .subscribe(function (data) {
            _this.ruter = data;
        }, function (error) {
            alert(error);
        });
    };
    EndreOrdre.prototype.onSubmit = function () {
        this.lagreOrdre();
    };
    EndreOrdre.prototype.lagreOrdre = function () {
        var _this = this;
        var nyOrdre = new ordre_1.ordre();
        nyOrdre.id = this.skjema.value.id;
        nyOrdre.refPers = this.skjema.value.refPers;
        nyOrdre.ruteNr = this.skjema.value.rute;
        nyOrdre.avgangNr = this.skjema.value.avgang;
        nyOrdre.antallBarn = this.skjema.value.antallBarn;
        nyOrdre.antallVoksen = this.skjema.value.antallVoksne;
        this.http.put('api/ordre', nyOrdre).subscribe(function (retur) {
            _this.router.navigate(["/visOrdre"]);
        }, function (error) {
            alert(error);
        });
    };
    EndreOrdre = __decorate([
        (0, core_1.Component)({
            templateUrl: "endreOrdre.html"
        }),
        __metadata("design:paramtypes", [http_1.HttpClient, forms_1.FormBuilder, router_1.Router, router_1.ActivatedRoute])
    ], EndreOrdre);
    return EndreOrdre;
}());
exports.EndreOrdre = EndreOrdre;
//# sourceMappingURL=endreOrdre.js.map