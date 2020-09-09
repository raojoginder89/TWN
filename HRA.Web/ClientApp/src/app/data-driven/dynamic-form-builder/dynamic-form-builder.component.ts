import { Component, Input, ComponentFactoryResolver } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { DynamicControlBase } from '../dynamic-control.base';

@Component({
    selector: 'app-dynamic-form-builder',
    templateUrl: './dynamic-form-builder.component.html',
    styleUrls: ['./dynamic-form-builder.component.scss']
})
export class DynamicFormBuilderComponent {
    @Input() question: DynamicControlBase<string>;
    @Input() form: FormGroup;

    get isValid() {
        return this.form.controls[this.question.key] instanceof FormControl ? this.form.controls[this.question.key].valid : true;
    }

    shouldShowErrors(): boolean {
        const control = this.form.controls[this.question.key];
        const isCOntrol = control instanceof FormControl;
        const reult = control && isCOntrol && (
            this.form.controls[this.question.key].errors
            && (this.form.controls[this.question.key].dirty || this.form.controls[this.question.key].touched));
        return reult;
    }

    getSubForm(formKey: string) {
        const form = this.form.controls[formKey];
        return form instanceof FormGroup ? form : null;
    }

    listOfErrors() {
        return Object.keys(this.form.controls[this.question.key].errors)
            .map(field => this.getMessage(field, this.form.controls[this.question.key].errors[field]));
    }

    private getMessage(type: string, params: any) {
        const currentValidator = this.question.validators.filter(field => {
            return field.validator['name'] === type;
        });

        return !!currentValidator && currentValidator.length > 0 ? currentValidator[0].errorMessage : '';
    }
}
