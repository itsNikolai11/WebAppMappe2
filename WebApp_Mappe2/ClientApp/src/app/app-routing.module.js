"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppRoutingModule = void 0;
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var adminpage_component_1 = require("./adminpage/adminpage.component");
var login_component_1 = require("./login/login.component");
var listeRute_1 = require("./rute/liste/listeRute");
var avgang_component_1 = require("./avgang/avgangListe/avgang.component");
var destinasjoner_component_1 = require("./destinasjon/destinasjonListe/destinasjoner.component");
var destinasjonLagre_1 = require("./destinasjon/destinasjonLagre/destinasjonLagre");
var destinasjonerRediger_component_1 = require("./destinasjon/destinasjonRediger/destinasjonerRediger.component");
var lagreRute_1 = require("./rute/lagre/lagreRute");
var lagreOrdre_1 = require("./ordre/lagre/lagreOrdre");
var visOrdre_1 = require("./ordre/liste/visOrdre");
var appRoots = [
    { path: 'adminpage', component: adminpage_component_1.AdminpageComponent },
    { path: 'login', component: login_component_1.LoginComponent },
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    { path: 'rute', component: listeRute_1.ListeRute },
    { path: 'avgang', component: avgang_component_1.AvgangComponent },
    { path: 'destinasjonListe', component: destinasjoner_component_1.DestinasjonComponent },
    { path: 'destinasjonLagre', component: destinasjonLagre_1.DestinasjonLagre },
    { path: 'destinasjonRediger/:id', component: destinasjonerRediger_component_1.DestinasjonRediger },
    { path: 'lagreRute', component: lagreRute_1.LagreRute },
    { path: 'nyOrdre', component: lagreOrdre_1.LagreOrdre },
    { path: 'visOrdre', component: visOrdre_1.VisOrdre }
    //Husk eks 'rediger/:id' send med f.eks id i rediger.
];
var AppRoutingModule = /** @class */ (function () {
    function AppRoutingModule() {
    }
    AppRoutingModule = __decorate([
        (0, core_1.NgModule)({
            imports: [
                router_1.RouterModule.forRoot(appRoots)
            ],
            exports: [
                router_1.RouterModule
            ]
        })
    ], AppRoutingModule);
    return AppRoutingModule;
}());
exports.AppRoutingModule = AppRoutingModule;
//# sourceMappingURL=app-routing.module.js.map