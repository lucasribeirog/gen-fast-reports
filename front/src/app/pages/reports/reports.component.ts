import { Component } from '@angular/core';

@Component({
  selector: 'app-reports',
  standalone: false,
  
  templateUrl: './reports.component.html',
  styleUrl: './reports.component.css'
})
export class ReportsComponent {
  reports: String[] = [
    'Relatório 1', 'Relatório 2', 'Relatório 3'
  ];
}
