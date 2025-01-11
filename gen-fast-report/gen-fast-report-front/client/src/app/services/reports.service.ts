import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Report } from '../types/report';
@Injectable({
  providedIn: 'root'
})
export class ReportsService {
  baseUrlReportService:string="https://localhost:7204/api/FastReportControllers"
  constructor(private http: HttpClient) { }

  getReports=():Observable<Report[]>=> this.http.get<Report[]>(this.baseUrlReportService)

  postReports=(newReport:Report):Observable<any>=> this.http.post(this.baseUrlReportService, newReport)

  putReports=(updatedReport:Report):Observable<any>=>this.http.put(this.baseUrlReportService, updatedReport)
  
  deleteReport=(id:number):Observable<any>=>this.http.delete(this.baseUrlReportService + "/${id}")
}
