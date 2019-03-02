import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css']
})
export class CardComponent implements OnInit {
  @Input() prescription;
  @Output() detail = new EventEmitter<number>();
  constructor() { }

  ngOnInit() {
  }

  details(number) {
    this.detail.emit(number);
  }
}
