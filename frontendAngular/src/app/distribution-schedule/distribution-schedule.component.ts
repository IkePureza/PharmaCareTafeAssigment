import { PrescriptionsService } from './../services/prescriptions.service';
import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatTableDataSource, MatPaginator, MatSort, MAT_DIALOG_DATA, MatDialog } from '@angular/material';
import { Http, Response } from '@angular/http';

@Component({
  selector: 'app-distribution-schedule',
  templateUrl: './distribution-schedule.component.html',
  styleUrls: ['./distribution-schedule.component.css']
})
export class DistributionScheduleComponent implements OnInit {

  patients: [];
  displayedColumns: string[] = ['Ward ID', 'Room ID', 'First Name', 'Last Name', 'Staff ID','Prescription','Date','Indoor','View'];
  dataSource = new MatTableDataSource;
  details: [];
  patient: any;
  day: number = 20;
  monthIndex: number;
  monthName: string = "November";
  year: number = 2018;
  constructor(private PrescriptionsService: PrescriptionsService, public dialog: MatDialog,private http: Http) {

  }

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  ngOnInit() {
    this.PrescriptionsService.getDistrubution().subscribe(data => {
      this.patients = data.body;
      console.log(data);
      this.dataSource = new MatTableDataSource(this.patients);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
    });
  
    //this.getAllDetails();
  }
  View(prescriptionID) {
    console.log('Fetching...');
    this.http.get('http://localhost:5001/api/prescriptions/' + prescriptionID + '/')
      .subscribe(
        (res: Response) => {
          const data = res.json();
          this.details = data.body.forward;
          this.patient = data.body.content;

          this.dialog.open(DistributionDialog, {
            data: this.details
          });
          console.log(this.details);
          console.log(this.patient);
        }
      )


  }
  previousDay() {
    if (this.day > 0) {
      this.day--;
    }
  }
  nextDay() {
    this.day++;
  }
  previousWeek() {
    this.day == this.day - 7;
  }
  nextWeek() {
    this.day == this.day + 7;
  }
  nextMonth() {
    this.monthIndex++;
  }
  previousMonth() {
    this.monthIndex--;
  }
  nextYear() {
    this.year++;
  }
  previousYear() {
    this.year--;

  }

  getAllDetails() {


  }
}

@Component({
  selector: 'distribution-dialog',
  templateUrl: 'distribution-dialog.html',
})
export class DistributionDialog {
  labelDetails: [];
  constructor(@Inject(MAT_DIALOG_DATA) public detailsData) { }

  //@ViewChild('content') content: ElementRef;
  
}