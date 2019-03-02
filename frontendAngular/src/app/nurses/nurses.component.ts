import { Component, Inject, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Router } from '@angular/router';
import { MatPaginator, MatTableDataSource, MatDialog, MAT_DIALOG_DATA, MatDialogConfig } from '@angular/material';
import * as jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import { PrescriptionsService } from '../services/prescriptions.service';

@Component({
  selector: 'app-nurses',
  templateUrl: './nurses.component.html',
  styleUrls: ['./nurses.component.css']
})
export class NursesComponent implements OnInit {

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



    // let doc = new jsPDF();

    // let specialElementHandlers = {
    //   '#editor': function(element, renderer){
    //     return true;
    //   }
    // };

    // let content = this.content.nativeElement;

    // doc.fromHTML(content.innerHTML, 15, 15, {
    //   'width': 190,
    //   'elementHandlers': specialElementHandlers

    // });

    // doc.save('test.pdf');

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

          this.dialog.open(NursesDialogue, {
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
  selector: 'nurses-dialogue',
  templateUrl: 'nurses-dialogue.html',
})
export class NursesDialogue {
  labelDetails: [];
  constructor(@Inject(MAT_DIALOG_DATA) public detailsData) { }
  }

