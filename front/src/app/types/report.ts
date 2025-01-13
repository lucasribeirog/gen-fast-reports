import { Areas } from "../enums/areas";

export class Report {
  id?: number;
  name?: string;
  area: Areas = Areas.None; 
  file?: string;

  constructor(init?: Partial<Report>) {
    Object.assign(this, init); 
  }
}