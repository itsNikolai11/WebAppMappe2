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
exports.AvgangComponent = void 0;
var core_1 = require("@angular/core");
var http_1 = require("@angular/common/http");
var router_1 = require("@angular/router");
var AvgangComponent = /** @class */ (function () {
    function AvgangComponent(_http, router) {
        this._http = _http;
        this.router = router;
    }
    AvgangComponent.prototype.ngOnInit = function () {
        this.hentRuter();
        this.hentAvganger();
    };
    AvgangComponent.prototype.hentRuter = function () {
        var _this = this;
        this._http.get("api/rute/")
            .subscribe(function (data) {
            _this.ruter = data;
            console.log(data);
        }, function (error) { return alert(error); }, function () { return console.log("ferdig get-/ruter"); });
    };
    AvgangComponent.prototype.hentAvganger = function () {
        var _this = this;
        this.laster = "Vennligst vent";
        this._http.get("api/Avgang/")
            .subscribe(function (data) {
            _this.alleAvganger = data;
            _this.laster = "";
            console.log(data);
        }, function (error) { return alert(error); }, function () { return console.log("ferdig get-/avganger"); });
    };
    AvgangComponent.prototype.onChange = function (event) {
        this.id = event;
        console.log(this.id);
    };
    AvgangComponent = __decorate([
        (0, core_1.Component)({
            selector: 'app-root',
            templateUrl: './avgang.component.html'
        }),
        __metadata("design:paramtypes", [http_1.HttpClient, router_1.Router])
    ], AvgangComponent);
    return AvgangComponent;
}());
exports.AvgangComponent = AvgangComponent;
//# sourceMappingURL=avgang.component.js.map