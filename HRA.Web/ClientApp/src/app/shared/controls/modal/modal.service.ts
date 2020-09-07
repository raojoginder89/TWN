import { Injectable, Input } from '@angular/core';
import { NgbModal, ModalDismissReasons, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { ModalOptions } from 'src/app/models/modalOptions';
import { ModalComponent } from './modal.component';

@Injectable({ providedIn: 'root' })
export class ModalService {
    modalOptions: NgbModalOptions;

    constructor(
        private modalService: NgbModal
    ) {
        this.modalOptions = {
            backdrop: 'static',
            backdropClass: 'customBackdrop',
            centered: true,
        };
    }

    open(options: ModalOptions) {
        const modalRef = this.modalService.open(ModalComponent, this.modalOptions);
        modalRef.componentInstance.options = options;
        return modalRef.result;
    }

    private getDismissReason(reason: any): string {
        if (reason === ModalDismissReasons.ESC) {
            return 'by pressing ESC';
        } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
            return 'by clicking on a backdrop';
        } else {
            return `with: ${reason}`;
        }
    }

}
