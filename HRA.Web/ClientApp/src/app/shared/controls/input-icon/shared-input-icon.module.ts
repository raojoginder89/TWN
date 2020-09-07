import { NgModule } from '@angular/core';
import { InputIconControl } from './input-icon.ctrl';
import { FormsModule } from '@angular/forms';
@NgModule({
    imports: [
        FormsModule
    ],
    declarations: [
        InputIconControl
    ],
    exports: [
        InputIconControl
    ]
})
export class SharedInputIconModule { }
