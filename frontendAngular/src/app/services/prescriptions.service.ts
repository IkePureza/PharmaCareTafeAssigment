import { Injectable } from '@angular/core';
import { Http, Response } from "@angular/http";
import { map } from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class PrescriptionsService {

  constructor(private http: Http) { }

  getPrescriptionsData() {
    return this.http.get('http://localhost:5001/api/prescriptions/active')
      .pipe(map(res => res.json()));
  }

  getDetailsData(){
    return this.http.get('http://localhost:5001/api/prescriptions/alldetails')
      .pipe(map(res => res.json()));
  }

  getPatients(){
    return this.http.get('http://localhost:5001/api/patients')
    .pipe(map(res => res.json()));
  }
  getDistrubution(){
    return this.http.get('http://localhost:5001/api/prescriptions/distribution/')
    .pipe(map(res => res.json()));
  }
}
