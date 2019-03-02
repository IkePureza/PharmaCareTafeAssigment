import { Component, Inject, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Router } from '@angular/router';
import { MatPaginator, MatTableDataSource, MatDialog, MAT_DIALOG_DATA, MatDialogConfig } from '@angular/material';
import * as jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import { PrescriptionsService } from '../services/prescriptions.service';

@Component({
  selector: 'app-pharmacist',
  templateUrl: './pharmacist.component.html',
  styleUrls: ['./pharmacist.component.css']
})
export class PharmacistComponent implements OnInit {

  prescriptions: [];
  selectedParam: any;
  constructor(private router: Router,
              private http: Http,
              public dialog: MatDialog,
           private PrescriptionsService: PrescriptionsService) { }
  displayedColumns: string[] = ['ID', 'Staff ID', 'Patient ID', 'Date', 'Details', 'View', 'Indoor', 'Print', 'Dispatch'];
  dataSource = new MatTableDataSource;
  details: [];
  printDetails: [];
  testing: any;
  patient: [];


  dispatchPrescription(prescriptionID, elm) {
    //TODO: open dialog to confirm dispatch
    this.http.put('http://localhost:5001/api/prescriptions/' + prescriptionID + '/dispatch/', null)
      .subscribe(
        (res: Response) => {
        }
      )
    this.dataSource.data = this.dataSource.data.filter(i => i !== elm)
  }



  //@ViewChild('content') content: ElementRef;
  printPrescription(prescriptionID) {
    this.printDetails = this.prescriptions[prescriptionID];
    console.log(this.printDetails);
    this.http.get('http://localhost:5001/api/prescriptions/' + prescriptionID + '/')
      .subscribe(
        (res: Response) => {
          const data = res.json();
          console.log(data);
          // console.log(this.printDetails);
          let patient = data.body.content;
          console.log(this.patient);
          html2canvas(document.getElementById('content'), data).then(function (canvas) {
            var img = canvas.toDataURL("image/png");
            var doc = new jsPDF();
            doc.addImage(img, 'JPEG', 5, 20);
            doc.save(data.body.content.firstName + data.body.content.lastName + 'prescription.pdf');
          });

        }
      )

  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
    console.log(this.dataSource)
  }

  View(prescriptionID) {
    console.log('Fetching...');
    this.http.get('http://localhost:5001/api/prescriptions/' + prescriptionID + '/')
      .subscribe(
        (res: Response) => {
          const data = res.json();
          this.details = data.body.forward;
          this.patient = data.body.content;

          this.dialog.open(DialogContentExampleDialog, {
            data: this.details
          });
          console.log(this.details);
          console.log(this.patient);
        }
      )


  }

  ngOnInit() {
    this.PrescriptionsService.getPrescriptionsData().subscribe(data => {
      this.prescriptions = data.body;
      console.log(data);
      this.dataSource = new MatTableDataSource(this.prescriptions);
    });
  }



}

@Component({
  selector: 'dialog-content-example-dialog',
  templateUrl: 'dialog-content-example-dialog.html',
})
export class DialogContentExampleDialog {
  labelDetails: [];
  constructor(@Inject(MAT_DIALOG_DATA) public detailsData) { }

  //@ViewChild('content') content: ElementRef;
  printLabel3() {

    console.log("Downloading...");
    let doc = new jsPDF();

    doc.setTextColor(0, 0, 255);
    doc.setFontSize(30);
    doc.text("Prescription Details ", 10, 10);
    doc.setTextColor(0, 0, 0);
    doc.setFontSize(16);
    doc.text("Times per day:  " + this.detailsData[0].forward[2].content.timesPerDay, 80, 20);
    doc.text("Drug Name:  " + this.detailsData[0].forward[2].content.name, 20, 40);
    doc.text("Drug Dose:  " + this.detailsData[0].forward[2].content.drugDose, 20, 50);
    doc.text("Start taking at:  " + this.detailsData[0].forward[2].content.firstTime, 120, 40);
    doc.text("Stop taking at:  " + this.detailsData[0].forward[2].content.lastTime, 120, 50);

    doc.save('ThirdDetails.pdf');
  }
  printLabel2() {

    console.log("Downloading...");
    let doc = new jsPDF();

    doc.setTextColor(0, 0, 255);
    doc.setFontSize(30);
    doc.text("Prescription Details ", 10, 10);
    doc.setTextColor(0, 0, 0);
    doc.setFontSize(16);
    doc.text("Times per day:  " + this.detailsData[0].forward[1].content.timesPerDay, 80, 20);
    doc.text("Drug Name:  " + this.detailsData[0].forward[1].content.name, 20, 40);
    doc.text("Drug Dose:  " + this.detailsData[0].forward[1].content.drugDose, 20, 50);
    doc.text("Start taking at:  " + this.detailsData[0].forward[1].content.firstTime, 120, 40);
    doc.text("Stop taking at:  " + this.detailsData[0].forward[1].content.lastTime, 120, 50);

    doc.save('SecondDetails.pdf');
  }
  printLabel1() {

    console.log("Downloading...");
    let contentString = "Drug Name: " + this.detailsData[0].forward[0].content.name + " " + "Drug Dose: " + this.detailsData[0].forward[0].content.drugDose + " " + "Start taking at:" + this.detailsData[0].forward[0].content.firstTime + " " + "Stop taking at:" + this.detailsData[0].forward[0].content.lastTime + "Times per day:" + this.detailsData[0].forward[0].content.timesPerDay;
    let doc = new jsPDF();

    doc.setTextColor(0, 0, 255);
    doc.setFontSize(30);
    doc.text("Prescription Details ", 10, 10);
    doc.setTextColor(0, 0, 0);
    doc.setFontSize(16);
    doc.text("Times per day:  " + this.detailsData[0].forward[0].content.timesPerDay, 80, 20);
    doc.text("Drug Name:  " + this.detailsData[0].forward[0].content.name, 20, 40);
    doc.text("Drug Dose:  " + this.detailsData[0].forward[0].content.drugDose, 20, 50);
    doc.text("Start taking at:  " + this.detailsData[0].forward[0].content.firstTime, 120, 40);
    doc.text("Stop taking at:  " + this.detailsData[0].forward[0].content.lastTime, 120, 50);



    doc.save('FirstDetails.pdf');
  }

}