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

  constructor() { }

  ngOnInit(): void {
  }

  onChangeSpec(event) {
    this.specChild.ngOnInit()
  }

  onChangeProd(event) {
    this.prodChild.ngOnInit()
  }

  onChangeCat($event: string) {
    this.catChild.updateCat($event)
  }
}
