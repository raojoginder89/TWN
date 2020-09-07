import { Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators, ValidatorFn } from '@angular/forms';
import { DynamicControlBase, ValidatorList } from './dynamic-control.base';


@Injectable()
export class DynamicControlService {
    constructor() { }

    toFormGroup(questions: DynamicControlBase<any>[]) {
        const form: FormGroup = new FormGroup({});
        return this.getFormGroup(questions, form);
    }

    getFormGroup(questions: DynamicControlBase<any>[], formGroup: FormGroup) {
        questions.forEach(question => {
            if (question.controlType === 'array') {
                const form: FormGroup = new FormGroup({});
                formGroup.addControl(question.key, this.getFormGroup(question.value as DynamicControlBase<any>[], form));
                return formGroup;
            }

            const control = !!question.validators && question.validators.length > 0
                ? new FormControl(question.value || '', Validators.compose(this.getValidators(question.validators)))
                : new FormControl(question.value || '');

            formGroup.addControl(question.key, control);
        });

        return formGroup;
    }

    getValidators(validators: ValidatorList[]): ValidatorFn[] {
        const validations: ValidatorFn[] = [];
        validators.forEach(function (item) {
            validations.push(item.validator);
        });

        return validations;
    }
}
