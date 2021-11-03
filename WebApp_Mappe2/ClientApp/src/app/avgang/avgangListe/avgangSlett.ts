import { Component } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  templateUrl: 'avgangSlett.html'
})
export class ModalAvgang {
  constructor(public modal: NgbActiveModal) { }
}
