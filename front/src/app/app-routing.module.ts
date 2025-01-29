import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { ReportsComponent } from './pages/reports/reports.component';
import { BalisticaComponent } from './pages/balistica/balistica.component';
import { ArmasComponent } from './pages/balistica/armas/armas/armas.component';
import { MunicoesComponent } from './pages/balistica/municoes/municoes/municoes.component';

const routes: Routes = [
  {path: '', component:HomeComponent},
  {path: 'reports/list', component: ReportsComponent},
  {path: 'balistica', component:BalisticaComponent},
  {path: 'armas', component: ArmasComponent},
  {path: 'municoes', component: MunicoesComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
