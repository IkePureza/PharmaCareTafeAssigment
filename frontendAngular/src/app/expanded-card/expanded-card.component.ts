import { Component, OnInit, Input } from '@angular/core';
import { Http, Response } from '@angular/http';
import { MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-expanded-card',
  templateUrl: './expanded-card.component.html',
  styleUrls: ['./expanded-card.component.css']
})
export class ExpandedCardComponent implements OnInit {
  @Input() prescription: any;
  @Input() details: any;
  
  constructor(private http: Http) { }
  det: any;
  status: any;
  indoor: any;
  creating: boolean = false;
  updating: boolean = false;

  drugID: any;
  drugDose: any;
  drugForm: any;
  firstTime: any;
  lastTime: any;
  timesPerDay: any;

  stage: number = 0;

  drugs: any;
  dangerous: boolean;
  message: string;
  
  ngOnInit() {
    this.http.get('http://localhost:5001/api/drugs/')
        .subscribe(
          (res: Response) => {
            const data = res.json();
            this.drugs = data.body;
          }
        )
  }

  checkDrugs() {
    var drugs = this.drugs;
    var details = this.details;
    this.dangerous = false;
    this.message = 'SAFE';
    details.forEach(element => {
      var name = element.name;
      drugs.forEach(element => {
        if (name === element.name) {
          if (element.dangerous) {
            this.dangerous = true;
            this.message = 'NOT SAFE!';
          }
        }
      });
    });
  }

  deleteDetail(ID) {
    this.http.delete('http://localhost:5001/api/details/'+ID+'/', null)
        .subscribe(
          (res: Response) => {
            const data = res.json();
          }
        )
  }

  setDrugID(ID) {
    this.drugID = ID;
  }

  newDetail(ID) {
    if (this.stage === 0 ) {
      this.creating = true;
      this.stage = 1;
    } else if (this.stage === 1) {
      this.creating = true;
      this.http.put('http://localhost:5001/api/details/'+ID+'/'+this.drugID+'/'+this.drugForm+'/'+this.drugDose+'/'+this.firstTime+'/'+this.lastTime+'/'+this.timesPerDay+'/', null)
        .subscribe(
          (res: Response) => {
            const data = res.json();
          }
        )
      this.creating = false;
      this.stage = 0;
    }
}
  

  update(ID) {
    if (this.updating) {
      this.updating = false;
    } else {
      this.updating = true;
    }
     this.http.put('http://localhost:5001/api/prescriptions/'+ ID +'/'+ this.det +'/'+ this.status +'/'+ this.indoor +'/', null)
         .subscribe(
           (res: Response) => {
             const data = res.json();
           }
         )
  }

  delete(ID) {
     this.http.delete('http://localhost:5001/api/drdash/'+ ID +'/', null)
         .subscribe(
           (res: Response) => {
             const data = res.json();
           }
         )
  }
}
