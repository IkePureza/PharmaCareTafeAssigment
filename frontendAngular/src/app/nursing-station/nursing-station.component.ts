import {
  Component,
  Inject,
  OnInit,
  ViewChild,
  ElementRef,
  AfterViewInit
} from '@angular/core';
import {
  Http,
  Response
} from '@angular/http';
import {
  Router
} from '@angular/router';
import {
  MatPaginator,
  MatTableDataSource,
  MatDialog,
  MAT_DIALOG_DATA,
  MatDialogConfig,
  MatSort
} from '@angular/material';
import * as jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import {
  PrescriptionsService
} from '../services/prescriptions.service';

@Component({
  selector: 'app-nursing-station',
  templateUrl: './nursing-station.component.html',
  styleUrls: ['./nursing-station.component.css']
})
export class NursingStationComponent implements OnInit  {
  

  patients: [];
  displayedColumns: string[] = ['Ward ID', 'Room ID', 'First Name', 'Last Name', 'View'];
  dataSource = new MatTableDataSource;
  details: [];
  patient: any;
  streamOfDataUpdates: any;
  private paginator: MatPaginator;
  private sort: MatSort;

  constructor(private router: Router,
    private http: Http,
    public dialog: MatDialog,
    private PrescriptionsService: PrescriptionsService) {

  }

  
  @ViewChild(MatSort) set matSort(ms: MatSort) {
    this.sort = ms;
    this.setDataSourceAttributes();
  }

  @ViewChild(MatPaginator) set matPaginator(mp: MatPaginator) {
    this.paginator = mp;
    this.setDataSourceAttributes();
  }

  setDataSourceAttributes() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;

    if (this.paginator && this.sort) {
      this.applyFilter('');
    }
  }

  
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue;
    console.log(this.dataSource)
  }
  View(patientID) {
    console.log('Fetching...');
    this.http.get('http://localhost:5001/api/prescriptions/' + patientID + '/')
      .subscribe(
        (res: Response) => {
          const data = res.json();
          this.details = data.body.forward;


          this.dialog.open(DialogDetails, {
            data: this.details
          });
          console.log(this.details);
        }
      )


  }

  printPrescription() {

    html2canvas(document.getElementById('table')).then(function (canvas) {
      var img = canvas.toDataURL("image/png");
      var doc = new jsPDF();
      doc.addImage(img, 'JPEG', 5, 7);
      doc.save('prescription.pdf');
    });



  }
  ngOnInit() {
    this.PrescriptionsService.getPatients().subscribe(data => {
      this.patients = data.body;
      console.log(data);
      this.dataSource = new MatTableDataSource(this.patients);

    });
  }
  


}

@Component({
  selector: 'dialog-details',
  templateUrl: 'dialog-details.html',
})
export class DialogDetails {
  labelDetails: [];
  constructor(@Inject(MAT_DIALOG_DATA) public detailsData) {}

  //@ViewChild('content') content: ElementRef;
}
