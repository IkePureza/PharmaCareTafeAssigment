import { Component, OnInit } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Router } from '@angular/router';


@Component({
  selector: 'app-doctor',
  templateUrl: './doctor.component.html',
  styleUrls: ['./doctor.component.css']
})
export class DoctorComponent implements OnInit {
  constructor(private router: Router, private http: Http) { }
  patientID: number;
  patient: string;
  prescriptionID: number;
  prescription: any;
  prescriptions: any;
  details: any;

  creating: boolean = false;
  stage: number = 0;

  staffID: number;
  prescriptionDetails: string;
  indoor: number;

  ngOnInit() {
    this.patientID = null;
    this.patient = null;
    this.prescriptionID = null;
    this.prescriptions = null;
    this.details = null;
    this.staffID = null;
    this.prescriptionDetails = null;
    this.indoor = null;
  }

  fetchPatient() {
    this.http.get('http://localhost:5001/api/patients/'+ this.patientID +'/')
      .subscribe(
        (res: Response) => {
          const data = res.json();
          this.patient = data.body;
        }
      )

    this.fetchPrescriptions();
  }

  fetchPrescriptions() {
    this.http.get('http://localhost:5001/api/drdash/'+ this.patientID +'/')
      .subscribe(
        (res: Response) => {
          const data = res.json();
          this.prescriptions = data.body;
        }
      )
  }

  onDetail(number) {
    this.prescriptionID = number;

    this.http.get('http://localhost:5001/api/drdash/single/'+ this.prescriptionID +'/')
        .subscribe(
          (res: Response) => {
            const data = res.json();
            this.prescription = data.body;
          }
        )

    this.http.get('http://localhost:5001/api/drdash/details/'+ number +'/')
      .subscribe(
        (res: Response) => {
          const data = res.json();
          this.details = data.body;
        }
      )
  }

  newPrescription() {
    if (this.stage === 0 ) {
      this.creating = true;
      this.stage = 1;
    } else if (this.stage === 1) {
      this.creating = true;
      var d = new Date();
      var month = (Number) (d.getMonth() + 1);
      var prescriptionDate = d.getDate()+'-'+ month +'-'+d.getFullYear();
      var temp = 'http://localhost:5001/api/prescriptions/'+this.patientID+'/'+this.staffID+'/'+prescriptionDate+'/'+this.prescriptionDetails+'/'+ 1 +'/'+this.indoor+'/';
      console.log(temp);
      this.http.put(temp, null)
        .subscribe(
          (res: Response) => {
            const data = res.json();
            this.details = data.body;
          }
        )
        this.creating = false;
        this.stage = 0;
    }
  }
}
