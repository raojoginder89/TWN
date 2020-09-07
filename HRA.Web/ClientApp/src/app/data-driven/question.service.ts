import { Injectable } from '@angular/core';

import { of } from 'rxjs';
import { DynamicControlBase } from './dynamic-control.base';
import { Validators } from '@angular/forms';

@Injectable()
export class QuestionService {

    // TODO: get from a remote source of question metadata
    getQuestions() {

        let questions1: DynamicControlBase<any>[] = [

            new DynamicControlBase<string>({
                controlType: 'dropdown',
                key: 'brave1',
                label: 'Bravery Rating',
                options: [
                    { key: 'solid', value: 'Solid' },
                    { key: 'great', value: 'Great' },
                    { key: 'good', value: 'Good' },
                    { key: 'unproven', value: 'Unproven' }
                ],
                order: 3
            }),

            new DynamicControlBase<number>({
                controlType: 'textbox',
                key: 'firstName1',
                label: 'First name',
                value: 1,
                validators: [
                    { validator: Validators.required, errorMessage: 'This field is required' },
                    { validator: Validators.email, errorMessage: 'Invalid Email' }],
                order: 1
            }),

            new DynamicControlBase<string>({
                controlType: 'textbox',
                key: 'emailAddress1',
                label: 'Email 1',
                type: 'email',
                order: 2
            })
        ];

        questions1 = questions1.sort((a, b) => a.order - b.order);

        const questions: DynamicControlBase<any>[] = [

            new DynamicControlBase<string>({
                controlType: 'dropdown',
                key: 'brave',
                label: 'Bravery Rating',
                options: [
                    { key: 'solid', value: 'Solid' },
                    { key: 'great', value: 'Great' },
                    { key: 'good', value: 'Good' },
                    { key: 'unproven', value: 'Unproven' }
                ],
                order: 3
            }),

            new DynamicControlBase<DynamicControlBase<any>[]>({
                controlType: 'array',
                key: 'form1',
                value: questions1
            }),

            new DynamicControlBase<number>({
                controlType: 'textbox',
                key: 'firstName',
                label: 'First name',
                value: 1,
                validators: [
                    { validator: Validators.required, errorMessage: 'This field is required' },
                    { validator: Validators.email, errorMessage: 'Invalid Email' }],
                order: 1
            }),

            new DynamicControlBase<string>({
                controlType: 'textbox',
                key: 'emailAddress',
                label: 'Email 1',
                type: 'email',
                order: 2
            })
        ];


        return of(questions.sort((a, b) => a.order - b.order));
    }
}
