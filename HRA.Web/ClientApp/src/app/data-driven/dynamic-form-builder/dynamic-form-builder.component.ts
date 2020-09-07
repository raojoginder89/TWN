import { Component, Input, ComponentFactoryResolver } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { DynamicControlBase } from '../dynamic-control.base';

@Component({
    selector: 'app-dynamic-form-builder',
    templateUrl: './dynamic-form-builder.component.html'
})
export class DynamicFormBuilderComponent {
    @Input() question: DynamicControlBase<string>;
    @Input() form: FormGroup;

    get isValid() {
        return this.form.controls[this.question.key] instanceof FormControl ? this.form.controls[this.question.key].valid : true;
    }

    shouldShowErrors(): boolean {
        return this.form.controls[this.question.key] &&
            this.form.controls[this.question.key].errors &&
            (this.form.controls[this.question.key].dirty || this.form.controls[this.question.key].touched);
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
