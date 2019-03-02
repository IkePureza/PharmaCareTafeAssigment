import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NavService {

  constructor() { }

  private navStateSource = new Subject<any>();
  navState$ = this.navStateSource.asObservable();

  setNavBarState( state: any ) {
    this.navStateSource.next( state );
  }
}
