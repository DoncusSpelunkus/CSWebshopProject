import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CurrentSpecs} from "../../../Entities/CurrentSpecs";

@Component({
  selector: 'app-current-spec-list',
  templateUrl: './current-spec-list.component.html',
  styleUrls: ['./current-spec-list.component.scss']
})
export class CurrentSpecListComponent implements OnInit {

  specList: CurrentSpecs[] = [];

  @Output()
  delete: EventEmitter<number> = new EventEmitter<number>();

  constructor() { }

  ngOnInit(): void {
  }

  updateNow(specList: CurrentSpecs[]): void{
    this.specList = specList;
    this.ngOnInit();
  }

  deleteSpec(id: number){
    this.delete.emit(id)
  }


}

