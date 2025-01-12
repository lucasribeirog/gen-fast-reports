import { Component, OnInit } from '@angular/core';
import { ReportService } from '../../services/report.service';
import { Report } from '../../types/report';

@Component({
  selector: 'app-reports',
  standalone: false,
  
  templateUrl: './reports.component.html',
  styleUrl: './reports.component.css'
})
export class ReportsComponent implements OnInit{
  reports?: Report[];
  
  constructor(private reportService:ReportService){ }
  
  ngOnInit(): void {
    this.fillReports()
  }

  fillReports(): void {
    this.reportService.getReports()
      .subscribe( (data :Report[]) => {
        this.reports = data
      });
  }
}
