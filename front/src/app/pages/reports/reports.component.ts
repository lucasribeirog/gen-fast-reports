import { Component, OnInit } from '@angular/core';
import { ReportService } from '../../services/report.service';
import { Report } from '../../types/report';
import { Areas } from '../../enums/areas';
import * as FileSaver from 'file-saver';
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
  
  getAreaDescription(value: number): string {
    return Areas[value];
  }
  base64ToUint8Array(base64: string): Uint8Array {
    const binaryString = atob(base64); // Decodifica de base64 para string binária
    const length = binaryString.length;
    const array = new Uint8Array(length);
    
    for (let i = 0; i < length; i++) {
      array[i] = binaryString.charCodeAt(i);
    }
    
    return array;
  }
  
  byteToFile(base64Data: string, fileName: string, fileType: string = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"): void {
    const docxData = this.base64ToUint8Array(base64Data);

    // Cria o Blob e faz o download
    const blob = new Blob([docxData], { type: fileType });
    FileSaver.saveAs(blob, fileName + ".docx");
  } 
  
}
