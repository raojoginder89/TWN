import { Component } from '@angular/core';
import { ICellRendererAngularComp } from 'ag-grid-angular';
import { IAfterGuiAttachedParams } from 'ag-grid-community';

@Component({
    selector: 'app-btn-cell-renderer',
    templateUrl: './btn.renderer.html'
})
export class BtnCellRendererComponent implements ICellRendererAngularComp {

    params: any;

    refresh(params: any): boolean {
        return true;
    }

    afterGuiAttached?(params?: IAfterGuiAttachedParams): void {

    }

    agInit(params: any): void {
        this.params = params;
    }

    btnClickedHandler(isDelete: boolean) {
        this.params.clicked(isDelete, this.params.value);
    }
}
