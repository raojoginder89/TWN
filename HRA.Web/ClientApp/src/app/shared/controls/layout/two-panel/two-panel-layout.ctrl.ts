import { Component, OnInit } from '@angular/core';

@Component({
    // tslint:disable-next-line: component-selector
    selector: 'two-panel-layout',
    templateUrl: './two-panel-layout.ctrl.html',
    styleUrls: ['./two-panel-layout.ctrl.scss']
})
// tslint:disable-next-line: component-class-suffix
export class TwoPanelLayoutControl {

    public brandingLogoSet = false;

    constructor() { }
}
