import { Http, Response } from '@angular/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import * as jsPDF from 'jspdf';
import html2canvas from 'html2canvas';

@Component({
  selector: 'app-staff',
  templateUrl: './staff.component.html',
  styleUrls: ['./staff.component.css']
})
export class StaffComponent implements OnInit {

  constructor(private router: Router, private http: Http) { }
  fName: string;
  lName: string;
  p: string;
  firstName: string;
  lastName: string;
  ward: string;
  room: string;
  prescriptionID: string;
  toFill: string;
  filled: string;
  date: string;
  time: string;
  indoor: string;

  prescription: [];
  ngOnInit() {
  }

  printOPD() {
    this.http.get('http://localhost:5001/api/OPD/' + this.fName + '/' + this.lName + '/')
      .subscribe(
        (res: Response) => {
          const data = res.json();
          this.p = data.body;
          this.prescriptionID = data.body.prescriptionID;
          this.toFill = data.body.toFill;
          this.filled = data.body.filledAndDispatched;
          this.date = data.body.dateDispatched;
          this.time = data.body.timeDispatched;
          this.indoor = data.body.indoorEmergency;
          console.log(this.p = data.body);
          if (this.toFill == "0") {
            this.toFill = "NO"
          }
          else{
            this.toFill = "YES "
          }
          if (this.filled == "0") {
            this.filled = "NO"
          }
          else{
            this.filled = "YES "
          }
          if (this.indoor == "0") {
            this.indoor = "NO"
          }
          else{
            this.indoor = "YES "
          }
      }
    )
  }

  enterPatientsDetails() {
    this.http.put('http://localhost:5001/api/OPD/' + this.firstName + '/' + this.lastName + '/' + this.ward + '/' + this.room + '/', null)
  .subscribe(
    (res: Response) => {
      this.firstName = "";
      this.lastName = "";
      this.ward = "";
      this.room = "";
    }
  )
  }

  Print() {

    console.log("Downloading...");
    let doc = new jsPDF();

    doc.setTextColor(0, 0, 255);
    doc.setFontSize(30);
    doc.text("OPD Prescription Details " , 10, 10);
    doc.setTextColor(0, 0, 0);
    doc.setFontSize(16);
    if (this.toFill == "0") {
      this.toFill = "NO"
    }
    else{
      this.toFill = "YES "
    }
    if (this.filled == "0") {
      this.filled = "NO"
    }
    else{
      this.filled = "YES "
    }
    if (this.indoor == "0") {
      this.indoor = "NO"
    }
    else{
      this.indoor = "YES "
    }
    doc.text("Prescription ID:  " + this.prescriptionID, 20, 30);
    doc.text("To Fill:  " + this.toFill, 20, 40);
    doc.text("Filled and Dispatched:  " + this.filled, 20, 50);
    doc.text("Date:  " + this.date, 20, 60);
    doc.text("Time:  " + this.time, 20, 70);
    doc.text("Indoor Emergency:  " + this.indoor, 20, 80);

    doc.save('OPDPrescription.pdf');
  }
}
