import {
    AfterViewInit,
    Component,
    ElementRef,
    forwardRef,
    HostListener,
    Input,
    Renderer2,
    ViewChild
} from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
    // tslint:disable-next-line: component-selector
    selector: 'input-icon',
    templateUrl: './input-icon.ctrl.html',
    styleUrls: ['./input-icon.ctrl.scss'],
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => InputIconControl),
            multi: true
        }
    ]
})
// tslint:disable-next-line: component-class-suffix
export class InputIconControl implements ControlValueAccessor, AfterViewInit {
    @Input()
    public icon = '';

    @Input()
    public type = 'text';

    @Input()
    public placeholder = '';

    @Input()
    public autocomplete = 'on';

    // tslint:disable-next-line: no-input-rename
    @Input('auto-focus')
    public autoFocus = false;

    public get value(): string {
        return this._value;
    }

    public set value(value: string) {
        this._value = value;
        this._propagateChange(this._value);
    }

    private _value = '';

    @ViewChild('input', {static: false})
    private _input: ElementRef;

    constructor(private _renderer: Renderer2, private _el: ElementRef) {}

    public ngAfterViewInit(): void {
        if (this.autoFocus) {
            setTimeout(() => {
                this._input.nativeElement.focus();
            }, 100);
        }
    }

    /**
     * Handles whenever the element is clicked. Used for testing
     *
     *
     * @memberOf InputIconControl
     */
    @HostListener('click', ['$event'])
    public handleClick(): void {
        this._input.nativeElement.focus();
    }

    // tslint:disable-next-line
    public writeValue(obj: any): void {
        this.value = obj;
    }

    // tslint:disable-next-line
    public registerOnChange(fn: any): void {
        this._propagateChange = fn;
    }

    // tslint:disable-next-line
    public registerOnTouched(fn: any): void {}

    public setDisabledState(isDisabled: boolean): void {
        this._renderer.setAttribute(this._el.nativeElement, 'disabled', isDisabled ? 'disabled' : '');
        this._renderer.setProperty(this._input.nativeElement, 'disabled', isDisabled);
        // disable other components here
    }

    private _propagateChange: Function = () => {};
}
