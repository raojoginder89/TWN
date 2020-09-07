import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { DynamicControlBase } from '../dynamic-control.base';
import { DynamicControlService } from '../dynamic-control.Service';

@Component({
    selector: 'app-dynamic-form',
    templateUrl: './dynamic-form.component.html'
})

export class DynamicFormComponent implements OnInit {

    @Input() questions: DynamicControlBase<string>[] = [];
    form: FormGroup;
    payLoad = '';

    constructor(private qcs: DynamicControlService) { }

    ngOnInit() {
        debugger;
        this.form = this.qcs.toFormGroup(this.questions);
    }

    onSubmit() {
        debugger;
        this.payLoad = JSON.stringify(this.form.getRawValue());
    }
}
