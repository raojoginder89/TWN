import { NgModule } from '@angular/core';
import { TwoPanelLayoutControl } from './two-panel/two-panel-layout.ctrl';
import { CommonModule } from '@angular/common';
import { LayoutFormControl } from './layout-form/layout-form.ctrl';
import { CopyrightControl } from './copy-right/copyright.ctrl';
import { HeroImageControl } from './hero-image/hero-image.ctrl';

@NgModule({
    imports: [
        CommonModule
    ],
    declarations: [
        TwoPanelLayoutControl,
        LayoutFormControl,
        CopyrightControl,
        HeroImageControl
    ],
    exports: [
        TwoPanelLayoutControl,
        LayoutFormControl,
        CopyrightControl,
        HeroImageControl
    ]
})

export class SharedLayoutModule { }