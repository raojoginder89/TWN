import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { DynamicControlBase } from '../dynamic-control.base';
import { DynamicControlService } from '../dynamic-control.Service';
import { Questions } from 'src/app/models/questions';
import { HRADetails } from 'src/app/models/hraDetails';

@Component({
    selector: 'app-dynamic-form',
    templateUrl: './dynamic-form.component.html',
    styleUrls: ['./dynamic-form.component.scss']
})

export class DynamicFormComponent implements OnInit {
    submitted = false;
    loading = false;
    @Input() questions: DynamicControlBase<string>[] = [];

    @Output()
    public savedJson: EventEmitter<HRADetails> = new EventEmitter<HRADetails>();

    form: FormGroup;
    payLoad = '';

    constructor(private qcs: DynamicControlService) { }

    ngOnInit() {
        this.form = this.qcs.toFormGroup(this.questions);
    }

    // private getObject(control: any, localObj: any) {
    //     if (control instanceof FormGroup) {
    //         debugger;
    //         control.forEach(element => {
    //             debugger;
    //             return this.getObject(element, localObj);
    //         });
    //     } else {
    //         localObj[control.key] = {key : control.Key, question: '', answer: control.value}
    //     }

    //     return localObj;
    // }

    onSubmit() {
        // const keys = Object.keys(this.form.getRawValue());
        // console.log(keys);
        // const _this = this;

        // const questionaires = {};
        // keys.forEach(x => {
        //     const control = _this.form.controls[x];
        //     let localObj = {};
        //     const localObject = this.getObject(control, localObj);
        //     questionaires[x] = localObject;

        // });

        this.submitted = true;
        this.payLoad = JSON.stringify(this.form.getRawValue());
        // reset alerts on submit
        this.form.markAllAsTouched();
        // stop here if form is invalid
        if (this.form.invalid) {
            return;
        }
        const grp = this.form.controls['demographicCharacteristics'] as FormGroup;
        const form = {
            ssn: grp.controls['ssn'].value,
            savedForm: JSON.stringify(this.payLoad)
        } as HRADetails;

        this.savedJson.emit(form);
    }
}
