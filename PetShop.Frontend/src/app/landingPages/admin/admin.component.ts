import {Component, OnInit, ViewChild} from '@angular/core';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

  @ViewChild('child') child;

  constructor() { }

  ngOnInit(): void {
  }

  onChange($event: string) {
    console.log($event)
    this.child.updateCat($event)
  }
}
