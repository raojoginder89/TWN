import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { AlertService } from 'src/app/services/AlertService.service';
import { first } from 'rxjs/operators';
import { GroupService } from 'src/app/services/group.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-add-group',
    templateUrl: './add-edit.component.html'
})
export class AddEditGroupComponent implements OnInit {
    groupForm: FormGroup;
    loading = false;
    submitted = false;

    @Input()
    groupId: string;

    constructor(
        private formBuilder: FormBuilder,
        private groupService: GroupService,
        private alertService: AlertService,
        private _router: Router
    ) { }

    ngOnInit() {
        // referenceId: string;
        this.groupForm = this.formBuilder.group({
            name: ['', Validators.required],
            email: ['', [Validators.required, Validators.email]],
            phoneNumber: ['', [Validators.required]],
            contactPerson: ['', Validators.required],
            address: ['', Validators.required],
            webSiteUrl: ['', Validators.required],
        });

        if (!!this.groupId) {
            this.groupService.getGroup(this.groupId).subscribe(group => {
                this.groupForm.patchValue(group);
            });
        }
    }

    // convenience getter for easy access to form fields
    get f() { return this.groupForm.controls; }

    onSubmit() {
        this.submitted = true;

        // reset alerts on submit
        this.alertService.clear();

        // stop here if form is invalid
        if (this.groupForm.invalid) {
            return;
        }

        this.loading = true;
        const groupRefrenceId = this.groupId;

        if (!!groupRefrenceId) {
            this.groupService.updateGroup(groupRefrenceId, this.groupForm.value)
                .pipe(first())
                .subscribe(
                    () => {
                        // return to list
                        this.alertService.success('Group updated uccessfully.');

                        this.loading = false;
                    },
                    error => {
                        if (error.error instanceof Array) {
                            error.error.map(err => this.alertService.error(err.value));
                        } else if (error.error instanceof Array) {
                            this.alertService.error(error.error);
                        } else {
                            this.alertService.error(error);
                        }
                        this.loading = false;
                    });
        } else {
            this.groupService.addGroup(this.groupForm.value)
                .pipe(first())
                .subscribe(
                    (group) => {
                        // return to list
                        this.alertService.success('Group added successfully.');
                        this.loading = false;
                        this.groupId = group.referenceId;
                        // this._router.navigate([`/groups/${group.referenceId}`])
                    },
                    error => {
                        if (error.error instanceof Array) {
                            error.error.map(err => this.alertService.error(err.value));
                        } else if (error.error instanceof Array) {
                            this.alertService.error(error.error);
                        } else {
                            this.alertService.error(error);
                        }
                        this.loading = false;
                    });
        }
    }
}


