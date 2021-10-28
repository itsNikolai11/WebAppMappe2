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
var destinasjoner_component_1 = require("./destinasjon/destinasjoner.component");
var login_component_1 = require("./login/login.component");
var listeRute_1 = require("./rute/liste/listeRute");
var appRoots = [
    { path: 'adminpage', component: adminpage_component_1.AdminpageComponent },
    { path: 'login', component: login_component_1.LoginComponent },
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    { path: 'destinasjon', component: destinasjoner_component_1.DestinasjonComponent },
    { path: 'rute', component: listeRute_1.ListeRute }
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