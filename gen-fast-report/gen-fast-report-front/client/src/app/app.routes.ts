import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { ReportsComponent } from './reports/reports.component';
import { UploadReportsComponent } from './upload-reports/upload-reports.component';
import { SettingsComponent } from './settings/settings.component';
import { HomeComponent } from './home/home.component';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' }, // Default route
  
  { path: 'login', component: LoginComponent },
  {path: 'reports', component: ReportsComponent},        // Reports route
  {path: 'upload-reports', component: UploadReportsComponent},        // Upload-Reports route
  {path: 'settings', component: SettingsComponent},        // Upload-Reports route
  // Add other routes here as needed
];
