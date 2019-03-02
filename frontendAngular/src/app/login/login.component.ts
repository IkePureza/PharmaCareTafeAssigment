import { NavService } from './../services/nav.service';
import { Http, Response } from '@angular/http';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../services/login.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  hide = true;
  constructor(
    private router: Router,
    private http: Http,
    private StaffService: LoginService,
    private NavBar: NavService
    ) { }
  username: string;
  password: string;
  staff: any;

  ngOnInit() {
  }
  

  login() {
    this.StaffService.getStaff(this.username, this.password).subscribe(staff => {
      this.staff = staff;
      //console.log(this.staff);
      if (this.staff.status == 404) {
        alert("Invalid credentials");
      } else if (this.staff.body.jobTitle === 6) {
        alert("Connection error");
      } else if (this.staff.body.jobTitle === 1) {
        this.router.navigate(["doctor"]);
      } else if (this.staff.body.jobTitle === 2) {
        this.router.navigate(["pharmacist"]);
      } else if (this.staff.body.jobTitle === 3) {
        this.router.navigate(["staff"]);
      } else if (this.staff.body.jobTitle === 4) {
        this.router.navigate(["nurses"]);
      } else if (this.staff.body.jobTitle === 5) {
        this.router.navigate(["admin"]);
      }
      this.StaffService.currentUser(this.staff);
      this.NavBar.setNavBarState(this.staff.body);
    });
  }
}