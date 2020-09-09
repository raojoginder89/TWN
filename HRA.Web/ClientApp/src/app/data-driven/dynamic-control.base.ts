import { Validators, ValidatorFn } from '@angular/forms';

export class DynamicControlBase<T> {
  value: T;
  key: string;
  label: string;
  validators: ValidatorList[];
  order: number;
  controlType: string;
  options: { key: string, value: string }[];
  showLabelInLine: boolean;
  labelClassName: string;
  controlClassName: string;
  containerClassName?: string;

  constructor(
    controlType?: string,
    key?: string,
    value?: T,
    label?: string,
    validators?: ValidatorList[],
    order?: number,
    options?: { key: string, value: string }[],
    showLabelInLine?: boolean,
    containerClassName?: string,
    labelClassName?: string,
    controlClassName?: string
  ) {
    this.value = value;
    this.key = key || '';
    this.label = label || '';
    this.validators = validators;
    this.order = order === undefined ? 1 : order;
    this.controlType = controlType || '';
    this.options = options || [];
    this.containerClassName = !!containerClassName ? containerClassName : 'col-md-12';
    this.labelClassName = labelClassName;
    this.controlClassName = controlClassName;
    this.showLabelInLine = showLabelInLine;

    if (!this.showLabelInLine) {
      if (!!this.labelClassName) {
        this.labelClassName += ' w-100';
      } else {
        this.labelClassName = 'w-100';
      }
    }
  }
}

export class ValidatorList {
  validator: ValidatorFn;
  errorMessage: string;
}
