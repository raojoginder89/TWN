// mymodal.component.ts
import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalOptions } from 'src/app/models/modalOptions';
@Component({
  selector: 'app-mymodal',
  templateUrl: './modal.component.html',
//   styleUrls: ['./modal.component.css']
})
export class ModalComponent implements OnInit {

  @Input() 
  options: ModalOptions;

  constructor(public activeModal: NgbActiveModal) {}

  ngOnInit() {
  }

}
