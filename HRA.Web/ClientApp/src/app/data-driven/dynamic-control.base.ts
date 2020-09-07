import { Validators, ValidatorFn } from '@angular/forms';

export class DynamicControlBase<T> {
  value: T;
  key: string;
  label: string;
  validators: ValidatorList[];
  order: number;
  controlType: string;
  type: string;
  options: { key: string, value: string }[];

  constructor(options: {
    value?: T;
    key?: string;
    label?: string;
    validators?: ValidatorList[];
    order?: number;
    controlType?: string;
    type?: string;
    options?: { key: string, value: string }[];
  } = {}) {
    this.value = options.value;
    this.key = options.key || '';
    this.label = options.label || '';
    this.validators = options.validators;
    this.order = options.order === undefined ? 1 : options.order;
    this.controlType = options.controlType || '';
    this.type = options.type || '';
    this.options = options.options || [];
  }
}

export class ValidatorList {
  validator: ValidatorFn;
  errorMessage: string;
}
