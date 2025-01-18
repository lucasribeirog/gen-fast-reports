import { Injectable } from '@angular/core';
import { enviroment } from '../../enviroments/enviroment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Report } from '../types/report';
@Injectable({
  providedIn: 'root'
})
export class ReportService {
  private apiUrl = enviroment.apiUrl;
  constructor(private http: HttpClient) {  }

  getReports():Observable<Report[]>{
    return this.http.get<Report[]>(this.apiUrl + '/StandardReport')
  }
}
