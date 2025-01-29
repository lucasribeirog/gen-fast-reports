import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { NavbarComponent } from './_components/navbar/navbar.component';
import { FooterComponent } from './_components/footer/footer.component';
import { BaseUiComponent } from './_components/base-ui/base-ui.component';
import { ReportsComponent } from './pages/reports/reports.component';
import { provideHttpClient } from '@angular/common/http';
import { BalisticaComponent } from './pages/balistica/balistica.component';
import { ArmasComponent } from './pages/balistica/armas/armas/armas.component';
import { MunicoesComponent } from './pages/balistica/municoes/municoes/municoes.component';
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavbarComponent,
    FooterComponent,
    BaseUiComponent,
    ReportsComponent,
    BalisticaComponent,
    ArmasComponent,
    MunicoesComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule
  ],
  providers: [
    provideHttpClient()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
