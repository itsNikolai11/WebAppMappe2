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
exports.DestinasjonRediger = void 0;
var core_1 = require("@angular/core");
var http_1 = require("@angular/common/http");
var destinasjon_1 = require("../../destinasjon");
var forms_1 = require("@angular/forms");
var router_1 = require("@angular/router");
var DestinasjonRediger = /** @class */ (function () {
    //TODO Konstruktør for http.
    function DestinasjonRediger(http, fb, route, router) {
        this.http = http;
        this.fb = fb;
        this.route = route;
        this.router = router;
        //TODO Validering av input med RegEx
        this.validering = {
            id: [""],
            sted: [
                null, forms_1.Validators.compose([forms_1.Validators.required, forms_1.Validators.pattern("[a-zA-ZøæåØÆÅ\\. ]{2,30}")])
            ],
            land: [
                null, forms_1.Validators.compose([forms_1.Validators.required, forms_1.Validators.pattern("[a-zA-ZøæåØÆÅ\\. ]{2,30}")])
            ]
        };
        this.skjema = fb.group(this.validering);
    }
    //TODO Onsubmit lagre knapp trykk kjør endre en 
    DestinasjonRediger.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.subscribe(function (params) {
            _this.redigerDest(params.id);
        });
    };
    DestinasjonRediger.prototype.vedSubmit = function () {
        this.redigerEnDest();
    };
    DestinasjonRediger.prototype.redigerDest = function (id) {
        var _this = this;
        this.http.get("api/destinasjon/" + id)
            .subscribe(function (destinasjon) {
            _this.skjema.patchValue({ id: destinasjon.id });
            _this.skjema.patchValue({ sted: destinasjon.sted });
            _this.skjema.patchValue({ land: destinasjon.land });
        }, function (error) { return console.log(error); });
    };
    DestinasjonRediger.prototype.redigerEnDest = function () {
        var _this = this;
        var redigertDest = new destinasjon_1.destinasjon();
        redigertDest.id = this.skjema.value.id;
        redigertDest.sted = this.skjema.value.sted;
        redigertDest.land = this.skjema.value.land;
        this.http.put("api/Destinasjon/", redigertDest)
            .subscribe(function (retur) {
            _this.router.navigate(['/destinasjonListe']);
        }, function (error) { return console.log(error); }, function () { return console.log("Redigert en destinasjon"); });
    };
    DestinasjonRediger = __decorate([
        (0, core_1.Component)({
            selector: 'app-root',
            templateUrl: "./destinasjonerRediger.component.html"
        }),
        __metadata("design:paramtypes", [http_1.HttpClient, forms_1.FormBuilder,
            router_1.ActivatedRoute, router_1.Router])
    ], DestinasjonRediger);
    return DestinasjonRediger;
}());
exports.DestinasjonRediger = DestinasjonRediger;
//# sourceMappingURL=destinasjonerRediger.component.js.map