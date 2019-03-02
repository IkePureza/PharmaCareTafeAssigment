import { Component, OnInit } from '@angular/core';
import { PrescriptionsService } from './../services/prescriptions.service';
import {MatButtonModule} from '@angular/material/button';
import { MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-preparation-list',
  templateUrl: './preparation-list.component.html',
  styleUrls: ['./preparation-list.component.css']
})
export class PreparationListComponent implements OnInit {

  detailsData: [];
  displayedColumns: string[] = ['ID', 'Drug Name', 'Drug Form', 'Drug Dose', 'Times per Day', 'Start', 'Finish'];
  dataSource = new MatTableDataSource;
  constructor(private PrescriptionsService: PrescriptionsService) {

   }

  ngOnInit() {
    this.PrescriptionsService.getDetailsData().subscribe(prescriptions => {

      this.detailsData = prescriptions.body;
      this.dataSource = new MatTableDataSource(this.detailsData);
    });
    //this.getAllDetails();
  }

  getAllDetails(){


  }
}