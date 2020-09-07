import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { UserService } from 'src/app/services/user.service';
import { AlertService } from 'src/app/services/AlertService.service';
import { MustMatch } from 'src/app/helpers/must-match.validators';

@Component({
    templateUrl: './reset-password.component.html',
    styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {
    resetPasswordForm: FormGroup;
    loading = false;
    submitted = false;

    private _token: string;
    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private userService: UserService,
        private alertService: AlertService
    ) {
        this.route.params.subscribe(params => { this._token = params['token']; });
    }

    ngOnInit() {
        this.resetPasswordForm = this.formBuilder.group({
            username: ['', [Validators.required, Validators.email]],
            newPassword: ['', [Validators.required, Validators.minLength(6)]],
            confirmPassword: ['', Validators.required],
            token: this._token
        }, {
            validator: MustMatch('newPassword', 'confirmPassword')
        });
    }

    // convenience getter for easy access to form fields
    get f() { return this.resetPasswordForm.controls; }

    onSubmit() {
        this.submitted = true;

        // reset alerts on submit
        this.alertService.clear();

        // stop here if form is invalid
        if (this.resetPasswordForm.invalid) {
            return;
        }

        this.loading = true;
        this.userService.resetPassword(this.resetPasswordForm.value)
            .pipe(first())
            .subscribe(
                () => {
                    this.alertService.success('Password reset successfully', true);
                    this.router.navigate(['/login']);
                },
                error => {
                    if (error.error instanceof Array) {
                        error.error.map(err => this.alertService.error(err.value));
                    } else {
                        this.alertService.error(error.error);
                    }
                    this.loading = false;
                });
    }
}
