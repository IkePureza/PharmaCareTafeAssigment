import { Component, OnInit } from '@angular/core';
import { Http, Response } from '@angular/http';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  patient: [];
  patientID: number;
  firstName: string;
  lastName: string;
  roomID: string;
  wardID: string;
  staff: [];
  staffID: number;
  sfirstName: string;
  slastName: string;
  susername: string;
  spassword: string;
  sjobTitle: string;
  sroomID: string;
  swardID: string;


  constructor( private http: Http) { }

  ngOnInit() {
  }

  fetchPatient() {
    this.http.get('http://localhost:5001/api/patients/'+ this.patientID +'/')
      .subscribe(
        (res: Response) => {
          const data = res.json();
          this.patient = data.body;
          this.patientID = data.body.patientID;
          this.firstName = data.body.firstName;
          this.lastName = data.body.lastName;
          this.roomID = data.body.roomID;
          this.wardID = data.body.wardID;
          console.log(this.patient);
          console.log(this.firstName);
        }
      )
  }


  insertPatient() {
    this.http.put('http://localhost:5001/api/patients/' + this.firstName + '/' + this.lastName + '/' + this.wardID + '/' + this.roomID + '/', null)
  .subscribe(
    (res: Response) => {}
  )
  }

  updatePatient() {
    this.http.put('http://localhost:5001/api/patients/' + this.patientID + '/' + this.firstName + '/' + this.lastName + '/' + this.wardID + '/' + this.roomID + '/', null)
  .subscribe(
    (res: Response) => {}
  )
  }

removePatient() {
    this.http.delete('http://localhost:5001/api/patients/' + this.patientID + '/')
  .subscribe(
    (res: Response) => {}
  )
  }

  
  fetchStaff() {
  this.http.get('http://localhost:5001/api/staff/' + this.staffID +'/')
    .subscribe(
      (res: Response) => {
        const data = res.json();
        this.patient = data.body;
        this.patientID = data.body.patientID;
        this.sfirstName = data.body.firstName;
        this.slastName = data.body.lastName;
        this.susername = data.body.username;
        this.spassword = data.body.password;
        this.sjobTitle = data.body.jobTitle;
        this.sroomID = data.body.roomID;
        this.swardID = data.body.wardID;
        console.log(this.patient);
        console.log(this.firstName);}
    )
  }

  insertStaff() {
    this.http.put('http://localhost:5001/api/staff/' + this.sfirstName + '/' + this.slastName + '/' + this.susername + '/' + this.spassword +  '/' + this.sjobTitle + '/' + this.swardID + '/' + this.sroomID + '/', null)
  .subscribe(
    (res: Response) => {}
  )
  }

  updateStaff() {
    this.http.put('http://localhost:5001/api/staff/' + this.staffID + '/' + this.sfirstName + '/' + this.slastName + '/' + this.susername + '/' + this.spassword + '/' + this.sjobTitle + '/' + this.swardID + '/' + this.sroomID + '/', null)
  .subscribe(
    (res: Response) => {}
  )
  }

  removeStaff() {
    this.http.delete('http://localhost:5001/api/staff/' + this.staffID + '/')
  .subscribe(
    (res: Response) => {}
  )
  }

}
