"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppModule = void 0;
var platform_browser_1 = require("@angular/platform-browser");
var core_1 = require("@angular/core");
var app_component_1 = require("./app.component");
var http_1 = require("@angular/common/http");
var login_component_1 = require("./login/login.component");
var forms_1 = require("@angular/forms");
var meny_1 = require("./meny/meny");
var listeRute_1 = require("./rute/liste/listeRute");
var adminpage_component_1 = require("./adminpage/adminpage.component");
var app_routing_module_1 = require("./app-routing.module");
var avgang_component_1 = require("./avgang/avgangListe/avgang.component");
var destinasjoner_component_1 = require("./destinasjon/destinasjonListe/destinasjoner.component");
var destinasjonLagre_1 = require("./destinasjon/destinasjonLagre/destinasjonLagre");
var destinasjonerRediger_component_1 = require("./destinasjon/destinasjonRediger/destinasjonerRediger.component");
var lagreRute_1 = require("./rute/lagre/lagreRute");
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        (0, core_1.NgModule)({
            declarations: [
                app_component_1.AppComponent,
                login_component_1.LoginComponent,
                meny_1.Meny,
                listeRute_1.ListeRute,
                adminpage_component_1.AdminpageComponent,
                avgang_component_1.AvgangComponent,
                destinasjoner_component_1.DestinasjonComponent,
                destinasjonLagre_1.DestinasjonLagre,
                destinasjonerRediger_component_1.DestinasjonRediger,
                lagreRute_1.LagreRute
                //Modal
            ],
            imports: [
                platform_browser_1.BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
                http_1.HttpClientModule,
                forms_1.ReactiveFormsModule,
                app_routing_module_1.AppRoutingModule,
                //NgbModule
            ],
            providers: [],
            bootstrap: [app_component_1.AppComponent],
            //entryComponents: [Modal]
        })
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map