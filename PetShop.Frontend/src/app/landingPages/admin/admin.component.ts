import {Component, OnInit, ViewChild} from '@angular/core';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

  @ViewChild('catList') catChild;
  @ViewChild('specList') specChild;
  @ViewChild('prodList') prodChild;
  @ViewChild('userList') userChild;

  constructor() { }

  ngOnInit(): void {
  }
  // Emits the changes given the child event
  onChangeSpec(event) {
    this.specChild.ngOnInit()
  }

  onChangeProd(event) {
    this.prodChild.ngOnInit()
  }

  onChangeCat($event: string) {
    this.catChild.updateCat($event)
  }

  onChangeUser(event) {
    this.userChild.updateList()
  }
}
