import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { ReportsComponent } from './pages/reports/reports.component';
import { BalisticaComponent } from './pages/balistica/balistica.component';

const routes: Routes = [
  {path: '', component:HomeComponent},
  {path: 'reports/list', component: ReportsComponent},
  {path: 'balistica', component:BalisticaComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
