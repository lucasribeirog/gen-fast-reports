import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { HomeComponent } from "./home/home.component";

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl:'./app.component.html',
  styleUrl:'./app.component.css',
  imports: [
    MatToolbarModule,
    MatButtonModule,
    RouterOutlet,
    HomeComponent
]
})
export class AppComponent {}
