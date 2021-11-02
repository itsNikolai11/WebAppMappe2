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
exports.VisOrdre = void 0;
var http_1 = require("@angular/common/http");
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var VisOrdre = /** @class */ (function () {
    function VisOrdre(http, router) {
        this.http = http;
        this.router = router;
    }
    VisOrdre.prototype.ngOnInit = function () {
        this.lastOrdre();
    };
    VisOrdre.prototype.lastOrdre = function () {
        var _this = this;
        this.http.get("api/ordre").subscribe(function (data) {
            _this.alleOrdre = data;
        });
    };
    VisOrdre.prototype.slettOrdre = function (id) {
        var _this = this;
        this.http.delete('api/ordre/' + id).subscribe(function (data) {
            _this.router.navigate(["/visOrdre"]);
        }, function (error) {
            alert(error);
        });
    };
    VisOrdre = __decorate([
        (0, core_1.Component)({
            templateUrl: "visOrdre.html"
        }),
        __metadata("design:paramtypes", [http_1.HttpClient, router_1.Router])
    ], VisOrdre);
    return VisOrdre;
}());
exports.VisOrdre = VisOrdre;
//# sourceMappingURL=visOrdre.js.map