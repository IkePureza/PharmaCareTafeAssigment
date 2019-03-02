import { Injectable } from '@angular/core';
import { Http, Response } from "@angular/http";
import { map } from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class LoginService {

    
  constructor(private http: Http) { }
  jobTitle: any;
  logginedUsername: string;
  logginedPassword: string;

  getStaff(username, password) {
    this.logginedPassword = password;
    this.logginedUsername = username;
    return this.http.get('http://localhost:5001/api/staff/' + username + '/' + password + '/')
      .pipe(map(res => res.json()));
  }

  currentUser(staff){
    this.jobTitle = staff;
  }
  
  test(){
      console.log("inside test", this.jobTitle);
  }

  

  Logout() {

  }

}
