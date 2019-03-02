import { Component, OnInit } from '@angular/core';
import { Http, Response } from '@angular/http';
import { MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-view-prescriptions',
  templateUrl: './view-prescriptions.component.html',
  styleUrls: ['./view-prescriptions.component.css']
})
export class ViewPrescriptionsComponent implements OnInit {
  details: [];
  patient: [];

  constructor(private http: Http,) { }
  displayedColumns2: string[] = ['ID', 'Staff ID', 'Patient ID', 'Date', 'Details', 'View', 'Indoor'];
  dispatchedPrescriptions: [];
  dataSource2 = new MatTableDataSource;

  applyFilter(filterValue: string) {
    this.dataSource2.filter = filterValue.trim().toLowerCase();
    console.log(this.dataSource2)
  }

  View(prescriptionID) {
    console.log('Fetching...');
    this.http.get('http://localhost:5001/api/prescriptions/' + prescriptionID + '/')
      .subscribe(
        (res: Response) => {
          const data = res.json();
          this.details = data.body.forward;
          this.patient = data.body.content;

          console.log(this.details);
          console.log(this.patient);
        }
      )


  }


  ngOnInit() {
    this.http.get('http://localhost:5001/api/prescriptions/inactive')
      .subscribe(
        (res: Response) => {
          const data = res.json();
          console.log(data);
          this.dispatchedPrescriptions = data.body;
          this.dataSource2 = new MatTableDataSource(this.dispatchedPrescriptions);
          //console.log(this.dataSource);
        }
      )


  }
  }


