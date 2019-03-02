import { Router } from '@angular/router';
import { LoginService } from '../services/login.service';
import { Component, OnInit } from '@angular/core';
import { NavService } from '../services/nav.service';



@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  currentPerson: any;
  firstName: string;
  userID: number;
  jobtitle: number;
  state: any;

  constructor(private router: Router, private CheckUser: LoginService, public NavBarService: NavService ) {
    this.NavBarService.navState$.subscribe( (state)=> this.state = state );
   }

  ngOnInit() {     
  }

  public updateNav() {

    this.test2();
    
  }

  test2(){
    console.log(this.state);
  }

  toPrescription(){
    this.router.navigate(["pharmacist"]);
  }

  toPrescriptionN(){
    this.router.navigate(["nurses"]);
  }

  toSchedule(){
    this.router.navigate(["pharmacist/distribution-schedule"]);

  }

  toPreparation(){
    this.router.navigate(["pharmacist/preparation-list"]);
  }

  toStation(){
    this.router.navigate(["pharmacist/nursing-station"]);
  }

  toStationN(){
    this.router.navigate(["nurses/nursing-station"]);
  }


  

  Logout(){
    this.NavBarService.setNavBarState(null);
    this.router.navigate([""]);
  }



}
